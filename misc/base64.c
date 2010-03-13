#include <stdio.h>
#include <stdlib.h>
#include <string.h>


unsigned char *  base64="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

union
{
	struct
	{
		unsigned long d:6;
		unsigned long c:6;
		unsigned long b:6;
		unsigned long a:6;
	}s;
	unsigned char	c[3];
}u;


unsigned char * encode(unsigned char *src,int srclen)
{
	int n,buflen,i,j;
	unsigned char *buf;
	static unsigned char *dst;

	buf=src;
	buflen=n=srclen;
	dst=(unsigned char*)malloc(buflen/3*4+3);
	memset(dst,0,buflen/3*4+3);
	for(i=0,j=0;i<buflen;i+=3,j+=4)  {
		dst[j]=(buf[i]&0xFC)>>2;
		dst[j+1]=((buf[i]&0x03)<<4) + ((buf[i+1]&0xF0)>>4);
		dst[j+2]=((buf[i+1]&0x0F)<<2) + ((buf[i+2]&0xC0)>>6);
		dst[j+3]=buf[i+2]&0x3F;
	}
	if(n%3==1) {
		j-=4,i-=3;
		dst[j]=(buf[i]&0xFC)>>2;
		dst[j+1]=((buf[i]&0x03)<<4);
		dst[j+2]=64;
		dst[j+3]=64;
		j+=4;
	}
	else if(n%3==2) {
		j-=4,i-=3;
		dst[j]=(buf[i]&0xFC)>>2;
		dst[j+1]=((buf[i]&0x03)<<4) + ((buf[i+1]&0xF0)>>4);
		dst[j+2]=((buf[i+1]&0x0F)<<2);
		dst[j+3]=64;
		j+=4;
	}
	for(i=0;i<j;i++) /* map 6 bit value to base64 ASCII character */
		dst[i]=base64[dst[i]];
	dst[j]=0;
	return dst;
}

unsigned char *decode(unsigned char *src,int * len)
{
	int n,i,j,pad;
	unsigned char *p;
	static unsigned char *dst;
	*len=0;
	pad=0;
	n=strlen(src);
	while(n>0&&src[n-1]=='=') {
		src[n-1]=0;
		pad++;
		n--;
	}
	for(i=0;i<n;i++)   { /* map base64 ASCII character to 6 bit value */
		p=strchr(base64,src[i]);
		if(!p)
			break;
		src[i]=p-base64;
	}

	dst=malloc(n*3/4+1);
	memset(dst,0,n*3/4+1);
	for(i=0,j=0;i<n;i+=4,j+=3)  {
		dst[j]=(src[i]<<2) + ((src[i+1]&0x30)>>4);
		dst[j+1]=((src[i+1]&0x0F)<<4) + ((src[i+2]&0x3C)>>2);
		dst[j+2]=((src[i+2]&0x03)<<6) + src[i+3];
		*len+=3;
	}
	*len-=pad;
	return dst;
}

#define BUF_SIZE 57
#define LINE_SIZE 80

int main(int argc,char * argv[])
{
	FILE * fp,*fpo;
	unsigned char * cdc;
	unsigned char buf[BUF_SIZE];
	unsigned char line[LINE_SIZE];
	char * nl,*eol;
	int r;
	if(argc<3) {
		fprintf(stderr,"Uasge: base64 [de] filename\n");
		return -1;
	}

	if(argv[1][0]=='e') { /*encode*/
		fp=fopen(argv[2],"rb");
		if(fp==NULL) {
			fprintf(stderr,"Can not open file %s\n",argv[2]);
			return -1;
		}
		while((r=fread(buf,sizeof(unsigned char),BUF_SIZE,fp))>0) {
			cdc=encode(buf,r);
			fprintf(stdout,"%s\n",cdc);
		}
		fclose(fp);
	}
	else if(argv[1][0]=='d') { /*decode*/
		if(argc<4) {
			fprintf(stderr,"Uasge: base64 d filename outname\n");
			return -1;
		}
		fp=fopen(argv[2],"r");
		fpo=fopen(argv[3],"wb");
		if(fp==NULL) {
			fprintf(stderr,"Can not open file %s\n",argv[2]);
			return -1;
		}
		while(fgets(line,LINE_SIZE,fp)) {
			nl=strchr(line,'\n');
			eol=strchr(line,'\r');
			if(nl)
				*nl=0;
			if(eol)
				*eol=0;
			r=strlen(line);
			if(r!=0) {
				cdc=decode(line,&r);
				fwrite(cdc,1,r,fpo);
			}
		}
		fclose(fp);
	}
	else {
		fprintf(stderr,"Uasge: base64 [de] filename\n");
		return -1;
	}

	return 0;
}
