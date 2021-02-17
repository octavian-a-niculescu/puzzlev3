using System;
using System.Collections;
using System.Collections.Generic;

namespace puzzle
{
    class RezolvarePuzzle
    {
        Node _nodInitiala;
        Node _nodFinal = null;
        Queue<Node> _frontiera = new Queue<Node>();
        int _noduriExplorate = 0;
        Dictionary<string, Node> _noduriGenerate = new Dictionary<string, Node>();

        public RezolvarePuzzle(Node nodInitiala)
        {
            _nodInitiala = nodInitiala;
        }

        public bool Rezolva()
        {
            // Nu are solutie
            if (!_nodInitiala.AreSolutie())
            {
                return false;
            }

            // Este deja rezolvat
            if (_nodFinal != null)
            {
                return true;
            }

            _frontiera.Enqueue(_nodInitiala);
            _noduriGenerate.Add(_nodInitiala.ToString(), _nodInitiala);

            do
            {
                var nodCurenta = _frontiera.Dequeue();
                _noduriExplorate++;

                if (nodCurenta.EsteSolutie())
                {
                    _nodFinal = nodCurenta;
                    break;
                }

                foreach (var nodUrmatoare in nodCurenta.GetNoduriUrmatoare())
                {
                    if (!_noduriGenerate.ContainsKey(nodUrmatoare.ToString()))
                    {
                        // O adaugam pentru explorare, doar daca nu a mai fost explorata
                        _frontiera.Enqueue(nodUrmatoare);
                        _noduriGenerate.Add(nodUrmatoare.ToString(), nodUrmatoare);
                    }
                }
            } while (_frontiera.Count > 0);

            return true;
        }

        public List<Node> GetPasiRezolvare()
        {
            var lista = new List<Node>();

            var nodCurent = _nodFinal;

            while (nodCurent != null)
            {
                lista.Add(nodCurent);
                nodCurent = nodCurent.nodAnterior;
            }

            lista.Reverse();

            return lista;
        }

        public Node nodFinal
        {
            get { return _nodFinal; }
        }

        public int NumarStariGenerate
        {
            get { return _noduriGenerate.Count; }
        }

        public int NumarStariExplorate
        {
            get { return _noduriExplorate; }
        }
    }
}
