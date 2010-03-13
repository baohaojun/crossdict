#include <cstdlib>
using namespace std;

unsigned long long mod2(unsigned long long a,unsigned long long b,unsigned long long n)
//计算a*b%n的，防止a*b的时候超过unsigned long long的范围
{
    unsigned long long sum=0;
    int i;
    for(i=61;i>=0;i--)
    {
        sum<<=1;
        if(b&(unsigned long long)1<<i)
            sum+=a;
        sum%=n;
    }
    return sum;
}
unsigned long long mod(unsigned long long a,unsigned long long b,unsigned long long n)
//模取幕，计算a^b%n；
{
    unsigned long long m=1;
    int i=60;
    while(!b&(unsigned long long)1<<i)
        i--;
    while(i>=0)
    {
        m=mod2(m,m,n);
        if(b&(unsigned long long)1<<i)
            m=mod2(m,a,n);
        i--;
    }
    return m;
}




bool witness(unsigned long long a,unsigned long long n)
{
    int i=0;
    unsigned long long t2,t=n-1;
    while(t%2==0)
    {
        t>>=1;
        i++;
    }
    t=mod(a,t,n);
    for(;i>0;i--)
    {
        t2=mod2(t,t,n);
        if(t2==1&&t!=1&&t!=n-1)
            return true;
        t=t2;
    }
    if(t!=1)
        return true;
    return false;
}
bool miller_rabin(unsigned long long n,int times)
{
    int i;
    unsigned long long t;
    for(i=0;i<times;i++)//其中10是测试的次数，越大出错率越小，一般应用取10就够
    {
        t=(double)rand()/RAND_MAX*(n-2)+1;
        if(witness(t,n))
        return false;
    }
    return true;
    //n是要测试的正数，如果是质数返回true，合数返回false
}
