﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Model
{
    public class Discount : Entity
    {
        public virtual string Type { get; set; }
        public virtual double Percent { get; set; }
    }
}
