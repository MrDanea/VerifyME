﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerifyME_Desktop.Models
{
    public class FNReconciliationModel
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool IsDirectory { get; set; }
        public ObservableCollection<FNReconciliationModel> Children { get; set; }
        public FNReconciliationModel() 
        {
            Children = new ObservableCollection<FNReconciliationModel> { };
        }
    }
}
