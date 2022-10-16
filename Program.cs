using System;
using System.Collections.Generic;

namespace Lesson13
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new OtusDictionary();
            for (int i = 0; i < 100; i++)
            {
                dict.Add(i, "test_" + i.ToString());
            }
            Console.WriteLine(dict.Get(34));



        }
    }

    public class OtusDictionary
    {
        private Entry[] _arr = new Entry[4];
        private List<Entry> _overArr = new List<Entry>();

        public string this [int index]
        {
            get
            {
                return _arr[index].value;
            }
            set
            {
                _arr[index].value = value;
            }
        }

        public OtusDictionary(int capasity)
        {
            Entry[] _arr = new Entry[capasity];
        }

        public OtusDictionary()
        {

        }
        public void Add(int key, string value)
        {
            var position = this.GetCoder(key);

            this.CheckValue(value);
            
            if (_arr[position].value is not null)
            {
                if(_arr[position].key == key)
                {
                    throw new ArgumentException("Данный ключ уже сущетсвует");
                }
                else
                {
                    _overArr.Add(new Entry { key = key, value = value });
                    this.Extend();
                    return;
                }

            }
            else
            {
                _arr[position].key = key;
                _arr[position].value = value;
            }

        }

        public string Get(int key)
        {
            var position = this.GetCoder(key);

            if (_arr[position].key == key)
            {
                return _arr[position].value;
            }
            else
            {
                throw new ArgumentException("Передаваемый ключ не найден в словаре");
            }
            
        }

        private void Extend()
        {
             Entry[] oldArr = new Entry[_arr.Length];
            _arr.CopyTo(oldArr, 0);
            _arr = new Entry[_arr.Length * 2];

            foreach (var pair in oldArr)
            {
                this.Add(pair.key, pair.value);
            }

            List<Entry> oldOverArr = new List<Entry>(_overArr);


            foreach (var pair in oldOverArr)
            {
                this.Add(pair.key, pair.value);
                _overArr.Remove(pair);
            }

        }

        private int GetCoder(int key) => key % _arr.Length;

        private void CheckValue(string value)
        {
            if (value == "")
            {
                throw new ArgumentNullException("Передаваемое значение не может быть пустым");
            }
        }

    }

    public struct Entry
    {
        public Entry(int key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public int key { get; set;}
        public string value { get; set; }

        public override string ToString() => $"({key}, {value})";
    }

}
