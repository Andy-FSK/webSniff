using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace webSniff
{
    public class WorkWait
    {
        Consumer c = new Consumer();
        Producer p = new Producer();
        public void maked()
        {
            p.maked();
        }
        public void used()
        {
            c.used();
        }
        public void wait()
        {
            int i = 0;
            int cSumTemp = c.sum;
            while ((p.sum - c.sum) > 12)
            {
                //让出时间片
                Thread.Sleep(1);
                if (c.sum == cSumTemp & i > 30)
                {

                    GC.Collect();
                    if (c.sum == cSumTemp)
                        p.sum = c.sum = 0;
                }
                else if (c.sum == cSumTemp)
                {
                    i++;
                }
                else
                {
                    i = 0;
                    cSumTemp = c.sum;
                }
            }
        }
        
    }
    class Producer
    {
        public int sum = 0;
        public void maked()
        {
            sum++;
        }
    }
    class Consumer
    {
        public int sum = 0;
        public void used() {
            sum++;
        }
    }
}
