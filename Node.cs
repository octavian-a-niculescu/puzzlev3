using System;
using System.Collections.Generic;
using System.Text;

namespace puzzle
{
    class Node:Stare
    {
        Node _nodAnterior;

        public Node nodAnterior { 
            get
            {
                return _nodAnterior;
            }
        }
        public Node(int[,] elemente, Node nodAnterior, Actiuni actiune) : base(elemente, actiune)
        {
            _nodAnterior = nodAnterior;
            _actiune = actiune;
            _elementZero = (int[])GetElementZero();
        }
        private Node GetNodNou(int offsetRand, int offsetColoana, Actiuni actiune)
        {
            var elementeNoi = (int[,])_elemente.Clone();

            int swap = elementeNoi[_elementZero[0] + offsetRand, _elementZero[1] + offsetColoana];
            elementeNoi[_elementZero[0] + offsetRand, _elementZero[1] + offsetColoana] = elementeNoi[_elementZero[0], _elementZero[1]];
            elementeNoi[_elementZero[0], _elementZero[1]] = swap;

            return new Node(elementeNoi, this, actiune);
        }

        public List<Node> GetNoduriUrmatoare()
        {
            var noduri = new List<Node>();

            // Jos
            if (_elementZero[0] < Randuri - 1)
            {
                var stareNoua = GetNodNou(1, 0, Actiuni.D);
                noduri.Add(stareNoua);
            }

            // Dreapta
            if (_elementZero[1] < Coloane - 1)
            {
                var stareNoua = GetNodNou(0, 1, Actiuni.R);
                noduri.Add(stareNoua);
            }

            // Stanga
            if (_elementZero[1] > 0)
            {
                var stareNoua = GetNodNou(0, -1, Actiuni.L);
                noduri.Add(stareNoua);
            }

            // Sus
            if (_elementZero[0] > 0)
            {
                var stareNoua = GetNodNou(-1, 0, Actiuni.U);
                noduri.Add(stareNoua);
            }

            return noduri;
        }

        public string Print()
        {
            var sb = new StringBuilder();
            foreach (int val in _elemente)
            {
                sb.Append(Convert.ToString(val));
                sb.Append(" ");
            }
            sb.Append("\n");
            sb.Append(_actiune);
            sb.Append("\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (int val in _elemente)
            {
                sb.Append(Convert.ToString(val));
                sb.Append('_');
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public Node NodAnterior
        {
            get { return _nodAnterior; }
        }
    }
}
