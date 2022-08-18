using System;

namespace Algoritm_BitАrithmetic
{
    class Program
    {
        static int[] bits;

        static void Main(string[] args)
        {
            
            // bitmask , where to go to the king            
            ulong maskKing = GetKingBitboardMoves(27);
            Console.WriteLine($" маска короля из позиции 27 = {maskKing} ");

            Console.WriteLine($"   кол-во ходов короля, вариант 1= {popcnt(maskKing)}");
            Console.WriteLine($"   кол-во ходов короля, вариант 2= {popcnt2(maskKing)}");
            FillBits();
            Console.WriteLine($"   кол-во ходов короля, вариант 3= {popcnt3(maskKing)}");
            Console.WriteLine();

            // bitmask , where to go to the horse 
            ulong maskHorse = GetHorseBitboardMoves(27);
            Console.WriteLine($" маска коня в позиции 27 = {maskHorse} ");
            Console.WriteLine($"   кол-во ходов коня , вариант 2 = {popcnt2(maskHorse)}");
            Console.WriteLine($" маска коня в позиции 24 = {GetHorseBitboardMoves(24)} ");
            Console.WriteLine($" маска коня в позиции 25 = {GetHorseBitboardMoves(25)} ");
            Console.WriteLine($" маска коня в позиции 30 = {GetHorseBitboardMoves(30)} ");
            Console.WriteLine($" маска коня в позиции 31 = {GetHorseBitboardMoves(31)} ");
   

            Console.WriteLine();
            // bitmask , where to go to the Rook 
            ulong maskRook = GetRookBitboardMoves(27);
            Console.WriteLine($" маска ладьи в позиции 27 = {maskRook} ");

        }

        static ulong GetKingBitboardMoves(int pos)
        {
            ulong K = 1UL << pos;
            ulong noL = 0xfefefefefefefefe;
            ulong noR = 0x7f7f7f7f7f7f7f7f;

            ulong KL = K & noL;
            ulong KR = K & noR;


            ulong mask =
                (KL << 7) | (K << 8) | (KR << 9) |
                (KL >> 1) | (KR << 1) |
                (KL >> 9) | (K >> 8) | (KR >> 7)
                ;
            return mask;
        }



        static ulong GetHorseBitboardMoves(int pos)
        {
            ulong K = 1UL << pos;
            ulong noL = 0xfefefefefefefefe;
            ulong noL2 = 0xFcFcFcFcFcFcFcFc;
            ulong noR = 0x7f7f7f7f7f7f7f7f;
            ulong noR2 = 0x3f3f3f3f3f3f3f3f;


            ulong mask = noR2 & (K << 8-2 | K >> 8+2)
                      | noR & (K << 8*2-1 | K >> 8*2+1)
                      | noL & (K << 8*2+1 | K >> 8*2-1)
                      | noL2 & (K << 8+2 | K >> 8-2);
            return mask;
        }


        static ulong GetRookBitboardMoves(int pos)
        {

            ulong horizon = 0xff;
            ulong vertical = 0x101010101010101;
            ulong R = 1UL << pos;

            ulong mask= horizon;


            while ((horizon & R) == 0)
            {
                horizon = horizon << 8;
            }
            while ((vertical & R) == 0)
            {
                vertical = vertical << 1;
            }


            return horizon ^ vertical;
        }

        static int popcnt(ulong mask)
        {
            int cnt = 0;

            while (mask>0)
            {
                if ((mask & 1) == 1)
                    cnt++;
                mask >>= 1;
            }

            return cnt;
        }

        static int popcnt2(ulong mask)
        {
            int cnt = 0;

            while (mask > 0)
            {
                cnt++;
                mask &= mask- 1;
            }

            return cnt;
        }

        private static void FillBits()
        {
            bits = new int[256];
            for (ulong  b = 0; b < 256; b++)
                bits[b] = popcnt2(b);
        }

        static int popcnt3(ulong mask)
        {
            int cnt = 0;

            while (mask > 0)
            {
                cnt+=bits[mask&255];
                mask >>=8;
            }

            return cnt;
        }
    }
}
