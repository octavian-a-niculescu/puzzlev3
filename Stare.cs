using System;
using System.Collections.Generic;
using System.Text;

namespace puzzle
{
    class Stare
    {
        public enum Actiuni
        {
            INIT,
            L,
            R,
            U,
            D
        }
        protected int[,] _elemente;
        public static readonly int _valoareElementZero = 0;
        protected int[] _elementZero;
        public Actiuni _actiune;

        public Stare(int[,] elemente, Actiuni actiune)
        {
            _elemente = (int[,])elemente.Clone();
            _actiune = actiune;
            _elementZero = (int[])GetElementZero();
        }

        protected Array GetElementZero()
        {
            for (int i = 0; i < Randuri; i++)
            {
                for (int j = 0; j < Coloane; j++)
                {
                    if (_elemente[i, j] == _valoareElementZero)
                    {
                        return new int[] { i, j };
                    }
                }
            }

            throw new Exception(String.Format("Lipseste elementul cu valoare {0}", _valoareElementZero));
        }

        public bool AreSolutie()
        {
            int nrInversiuni = 0;
            for (int i = 0; i < Randuri; i++)
            {
                for (int j = 0; j < Coloane; j++)
                {
                    for (int p = i; p < Randuri; p++)
                    {
                        for (int t = (p == i ? j + 1 : 0); t < Coloane; t++)
                        {
                            if (_elemente[i, j] > _elemente[p, t] && _elemente[i, j] != _valoareElementZero && _elemente[p, t] != _valoareElementZero)
                            {
                                nrInversiuni++;
                            }
                        }
                    }
                }
            }
            if (Coloane % 2 == 1 && nrInversiuni % 2 == 0 || (Coloane % 2 == 0 && (((Coloane - _elementZero[1]) % 2 == 1) == (nrInversiuni % 2 == 0))))
            {
                return true;
            }
            return false;
        }

        public bool EsteSolutie()
        {
            int prevVal = 0;

            for (int i = 0; i < Randuri; i++)
            {
                for (int j = 0; j < Coloane; j++)
                {
                    int val = _elemente[i, j];

                    if (val > prevVal
                        || (val == _valoareElementZero && i == Randuri - 1 && j == Coloane - 1))
                    {
                        prevVal = val;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        public int Randuri
        {
            get { return _elemente.GetLength(0); }
        }

        public int Coloane
        {
            get { return _elemente.GetLength(1); }
        }
    }
}
