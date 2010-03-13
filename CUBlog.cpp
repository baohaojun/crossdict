/*
 *

Copyright 2009 http://code.google.com/p/toolkits/. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:
  * Redistributions of source code must retain the above copyright
    notice, this list of conditions and the following disclaimer.
  * Redistributions in binary form must reproduce the above
    copyright notice, this list of conditions and the following
    disclaimer in the documentation and/or other materials provided
    with the distribution.
  * Neither the name of http://code.google.com/p/toolkits/ nor the names of its
    contributors may be used to endorse or promote products derived
    from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

#include <iostream>
#include <string>
#include <queue>
#include <sstream>
#include <boost/asio.hpp>

#include <boost/lexical_cast.hpp>
#include <boost/regex.hpp>
#include <boost/tuple/tuple.hpp>
#include <boost/date_time/gregorian/gregorian.hpp>
#include <boost/date_time/posix_time/posix_time.hpp>

using namespace std;
using namespace boost::gregorian;
using namespace boost::posix_time;

using boost::asio::ip::tcp;

typedef boost::tuple<string,string,string> element;

void FormatHTML(queue<element> &q) {

	string art_base="/u/8780/showart.php?id=";

	cout<<"<html>\n<head>"<<endl;
	cout<<"<title>Linxh's CUBlog List</title>"<<endl;
	cout<<"<meta http-equiv=Content-Type content=\"text/html;charset=gb2312\">"<<endl;
	cout<<"</head>\n<body>"<<endl;
	cout<<"<font face=\"Arial, Helvetica\" size=4>"<<endl;
	//cout<<""<<now<<"<br>"<<endl;
	cout<<""<<q.size()<<"<br><br>\n"<<endl;
	cout<<"<table>\n";

	int i=1;
	while(!q.empty()) {
		element e=q.front();
		q.pop();
		string art_url=art_base+e.get<0>();
		string fname=boost::tuples::get<1>(e);
		string date=boost::tuples::get<2>(e);
		cout<<"<tr><td>"<<endl;
		cout<<i++<<"</td><td><a target=\"_blank\" href=\""<<art_url<<"\">"<<fname<<"</a></td><td>"<<date<<"</td></tr>"<<endl;
	}
	cout<<"</table>"<<endl;
	cout<<"</font>"<<endl;
	cout<<"</body>\n</html>"<<endl;
}


int main(int argc,char * argv[]) {

	string list_base="/u/8780/article.php?frmid=0&page=";
	string art_base="/u/8780/showart.php?id=";

	boost::regex rexp("<a href=\"showart_([0-9]{6}).*?><font.*?><b>(.*?)</b></font></a>\n</td>\n<td.*?>([0-9-]*)</td>");
	boost::regex fnp("[/\\:*?\"<>]");


	queue<element> q;
	bool save=true;
	if(argc==1)
		save=false;

	date today=day_clock::local_day();
	ptime now = second_clock::local_time();


	boost::asio::io_service io_service;
	tcp::resolver resolver(io_service);
	tcp::socket socket(io_service);

	tcp::resolver::query query("blog.chinaunix.net", "http");
	tcp::resolver::iterator endpoint_iterator = resolver.resolve(query);
	tcp::resolver::iterator end;

	boost::system::error_code error = boost::asio::error::host_not_found;

	boost::asio::streambuf request;
	std::ostream request_stream(&request);
	boost::asio::streambuf response;
	std::istream response_stream(&response);
	std::string http_version;
	unsigned int status_code;
	std::string status_message;


	try {
		int i=0;
		bool cond=true;
		while(cond) {
			i++;
			cond=false;
			string list_url=list_base+boost::lexical_cast<string>(i);
			cerr<<list_url<<endl;

			socket.connect(*endpoint_iterator, error);
			if(error) {
				cerr<<"connect error"<<endl;
				throw boost::system::system_error(error);
			}

			//get the page content
			request_stream << "GET "<<list_url<<" HTTP/1.1\r\n";
			request_stream << "Host: blog.chinaunix.net\r\n";
			request_stream << "Accept: */*\r\n";
			request_stream << "Connection: close\r\n\r\n";

			// Send the request.
			boost::asio::write(socket, request);
			// Read the response status line.
			boost::asio::read_until(socket, response, "\r\n");
			// Check that response is OK.
			response_stream >> http_version;
			response_stream >> status_code;
			std::getline(response_stream, status_message);
			if (!response_stream || http_version.substr(0, 5) != "HTTP/") {
				std::cout << "Invalid response\n";
				return 1;
			}
			if (status_code != 200) {
				std::cout << "Response returned with status code " << status_code << "\n";
				return 1;
			}
			// Read the response headers, which are terminated by a blank line.
			boost::asio::read_until(socket, response, "\r\n\r\n");

			// Process the response headers.
			std::string header;
			while (std::getline(response_stream, header) && header != "\r")
				;//std::cout << header << "\n";
			std::cout << "\n";


			stringstream ss;
			// Write whatever content we already have.
			if (response.size() > 0) {
				ss << &response;
			}

			// Read until EOF.
			while (boost::asio::read(socket, response,boost::asio::transfer_at_least(1), error))
				ss<<&response;
			if (error != boost::asio::error::eof)
				throw boost::system::system_error(error);

			string s=ss.str();

			// Get the id and article name pairs
			string::const_iterator it=s.begin();
			string::const_iterator end=s.end();
			boost::smatch m;
			while(boost::regex_search(it,end,m,rexp)) {
				cond=true;
				q.push(element(m[1].str(),m[2].str(),m[3].str()));
				it=m[0].second;
			}
			socket.close();
		}

		FormatHTML(q);
	}
	catch(exception &e) {
		cerr<<"EXCEPTION: "<<e.what()<<endl;
	}

	return 0;
}
