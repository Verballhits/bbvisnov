﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBVisNov
{
    public class QuestManager
    {
        private List<Quest> ActiveQuests;
        private List<Quest> CompletedQuests;
        private List<Quest> FailedQuests;

        public QuestManager()
        {
            ActiveQuests = new List<Quest>();
            CompletedQuests = new List<Quest>();
            FailedQuests = new List<Quest>();
        }
    }
}