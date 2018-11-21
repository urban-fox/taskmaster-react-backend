using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMasterApi.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CourseCode { get; set; }

        private List<Topic> Topics;

        public List<Topic> GetTopics()
        {
            return Topics;
        }
        public void AddTopic(Topic newTopic)
        {
            Topics.Append(newTopic);
        }
        public void DeleteTopic(int index)
        {
            Topics.RemoveAt(index);
        }


    }
}
