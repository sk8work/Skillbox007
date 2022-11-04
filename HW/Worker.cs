//ID - авто
//Дата и время добавления записи - авто
//Ф.И.О.
//Возраст
//Рост
//Дата рождения
//Место рождения

using System;
using System.Collections.Generic;
using System.Text;

namespace HW
{
    struct Worker
    {
        //public string ID { get; set; }
        public DateTime DateTimeInsert { get; set; }
        public string   FIO { get; set; }
        public string   Age { get; set; }
        public string   Height { get; set; }
        public DateTime BDate { get; set; }
        public string   BPlace { get; set; }
    }
}
