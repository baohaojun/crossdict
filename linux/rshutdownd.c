/*
 *	rshutdownd.c
 *
 *	remote shutdown Linux daemon

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

#include <string.h>
#include <stdio.h>

#include <fcntl.h>
#include <sys/resource.h>
#include <signal.h>
#include <stdlib.h>
#include <shadow.h>

#define _XOPEN_SOURCE
#include <unistd.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <netinet/in.h>

int auth(const char * username,const char * password) {
	struct spwd * s=getspnam(username);
	char * cp;
	if(s==NULL) return 0;
	cp=(char*)crypt(password,s->sp_pwdp);
	if(strcmp(cp,s->sp_pwdp)==0) {
		return 1;
	}
	else {
		return 0;
	}
}

void daemonize() {
	int i,fd0,fd1,fd2;
	pid_t pid;
	struct rlimit r1;
	struct sigaction sa;
	umask(0);

	if(getrlimit(RLIMIT_NOFILE,&r1)<0) {
	}
	if((pid=fork())<0) {
	}
	else if(pid!=0) {
	        exit(0);
	}

	setsid();

	sa.sa_handler=SIG_IGN;
	sigemptyset(&sa.sa_mask);
	sa.sa_flags=0;
	if(sigaction(SIGHUP,&sa,NULL)<0) {
	}
        if((pid=fork())<0) {
        }
        else if(pid>0) {
		exit(0);
	}
       if(chdir("/")<0) {
       }
       if(r1.rlim_max==RLIM_INFINITY) {
	       r1.rlim_max=1024;
       }
       for(i=0;i<r1.rlim_max;i++) {
	       close(i);
       }
       fd0=open("/dev/null",O_RDWR);
       fd1=dup(0);
       fd2=dup(0);
}

int trim(char * str) {
	int size=0;
	while(*str) {
		size++;
		if(*str==10 || *str==13) {
			*str=0;
			size--;
		}
		str++;
	}
	return size;
}

#define PORT 16800

int main()
{
	int sock;
	int acc;
	struct sockaddr_in server;
	struct sockaddr_in client;
	int len;
	int size;
	char buf[1024];
	char username[1024];
	char pass[1024];
	char cmd[1024];

	daemonize();

	sock=socket(AF_INET,SOCK_STREAM,0);
	if(sock<0) {
		perror("Can not create socket");
		return -1;
	}

	bzero(&server,sizeof(server));
	server.sin_family=AF_INET;
	server.sin_addr.s_addr=htonl(INADDR_ANY);
	server.sin_port=htons(PORT);

	int r=bind(sock,(struct sockaddr*)&server,sizeof(server));
	if(r<0) {
		perror("Can not bind address");
		return -1;
	}
	listen(sock,10);

	for(;;) {
		len = sizeof(client);
		acc = accept(sock,(struct sockaddr*)&client,&len);
		//Now began the protocol
		bzero(buf,sizeof(buf));
		size = read(acc,buf,sizeof(buf));
		size = trim(buf);
		if (size==1 && buf[0]=='u') {
			//read the username
			bzero(username,sizeof(username));
			size = read(acc,username,sizeof(username));
			size = trim(username);
			printf("user: %s\n",username);
		} else {
			continue;
		}
		bzero(buf,sizeof(buf));
		size = read(acc,buf,sizeof(buf));
		size = trim(buf);
		if (size==1 && buf[0]=='p') {
			//read the password
			bzero(pass,sizeof(pass));
			size = read(acc,pass,sizeof(pass));
			size = trim(pass);
			printf("password: %s\n",pass);
		} else {
			continue;
		}
		if(auth(username,pass)) {
			//get the command
			bzero(cmd,sizeof(cmd));
			size = read(acc,cmd,sizeof(cmd));
			size = trim(cmd);
			printf("%s\n",username);
			printf("%s\n",pass);
			printf("%s\n",cmd);
			char * command;
			if(strcmp("-r",cmd)==0) {
				if(execl("shutdown","-r","now",(char*)0)<0) {
					perror("Can not reboot");
				}
			} else if(strcmp("-h",cmd)==0) {
				if(execl("shutdown","-h","now",(char*)0)<0) {
					perror("Can not shutdown");
				}
			} else {
				char msg[]="Bad Command\n";
				write(acc,msg,sizeof(msg));
				close(acc);
			}
		} else {
			char msg[]="Auth Error\n";
			write(acc,msg,sizeof(msg));
			close(acc);
		}
	}
	return 0;
}
