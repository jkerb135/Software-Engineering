using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Classes
{
    public class Task
    {
        #region Properties

        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string IsActive { get; set; }

        #endregion

        #region Constructors

        public Task()
        {
            this.TaskID = 0;
            this.TaskName = String.Empty;
            this.IsActive = String.Empty;
        }

        #endregion

        public void CreateTask(int CategoryID)
        {

        }

        public void UpdateTask()
        {

        }

        public void DeleteTask()
        {

        }

        public void CompleteTask()
        {
        }

        public void AddTimeToTask(double Minutes)
        {
        }

        public int GetNumberOFTasksComplete(string Username)
        {
            int NumberOfTasksComplete = 0;

            return NumberOfTasksComplete;
        }

        public List<Task> GetIncompleteTasks(string Username)
        {
            List<Task> IncompleteTasks = new List<Task>();

            return IncompleteTasks;
        }

        public List<Task> GetAssignedTasks(string Username)
        {
            List<Task> AssignedTasks = new List<Task>();

            return AssignedTasks;
        }

        public List<Task> GetTasksInCategory(int CategoryID)
        {
            List<Task> TasksInCategory = new List<Task>();

            return TasksInCategory;
        }

        public List<Task> GetTasksInCategoryAssignedToUser(int CategoryID, string Username)
        {
            List<Task> TasksInCategoryAssignedToUser = new List<Task>();

            return TasksInCategoryAssignedToUser;
        }

        public void AssignUserToTask(string Username)
        {

        }

        public void UnAssignUserFromTask(string Username)
        {

        }

        public void AssignTaskToCategory(int CategoryID)
        {

        }
    }
}