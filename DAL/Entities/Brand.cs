﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Brand : AbsModel
    {
        public byte[] Photo { get; set; }
        public List<Model> Models { get; set; }

        public Brand()
        {
            Models = new List<Model>();
        }
    }
}
