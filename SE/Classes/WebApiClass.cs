using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SE.Models;

namespace SE.Classes
{
    public class WebApiClass
    {
        public new class User
        {
            public Guid ApplicationId { get; set; }
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public bool IsAnonymous { get; set; }
            public DateTime LastActivityDate { get; set; }
            public string Password { get; set; }
        }
        public class NewTask
        {
            public int TaskId { get; set; }
            public string TaskName { get; set; }
            public ICollection<TaskAssignment> Assignments { get; set; }

        }

        public class NewCategory
        {
            public string CategoryName { get; set; }
            public int CategoryId { get; set; }
            public List<UserTasks> Tasks { get; set; }
        }

        public class NewCompletedMainStep
        {
            public int MainStepId { get; set; }
            public string AssignedUser { get; set; }
        }

        public class NewMainStep
        {
            public string MainStepName { get; set; }
            public int MainStepId { get; set; }
            public string MainStepText { get; set; }
            public string VideoPath { get; set; }
            public string AudioPath { get; set; }
            public int SortOrder { get; set; }
            public List<NewCompletedMainStep> CompletedMainSteps { get; set; }
            public List<NewDetailedStep> DetailedSteps { get; set; }
        }

        public class NewDetailedStep
        {
            public int DetailedStepId { get; set; }
            public string DetailedStepName { get; set; }
            public string ImagePath { get; set; }
            public string DetailedStepText { get; set; }
        }

        public class UserTasks
        {
            public int TaskId { get; set; }
            public string TaskName { get; set; }
        }

        public class Task
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int TaskId { get; set; }
            public string TaskName { get; set; }
            public string AssignedUser { get; set; }

        }
        public class UserRequest
        {
            public string AssignedSupervisor { get; set; }
            public string User { get; set; }
            public string TaskName { get; set; }
            public string TaskDesc { get; set; }
            public DateTime CompleteBy { get; set; }
        }
        public class SendUser
        {
            public string Username { get; set; }
            public string IpAddress { get; set; }
            public bool SignedIn { get; set; }
        }
        public class Completed
        {
            public int MainStepId { get; set; }
            public int TaskId { get; set; }
            public string MainStepName { get; set; }
            public string AssignedUser { get; set; }
            public DateTime DateTimeComplete { get; set; }
            public float TotalTime { get; set; }
        }
        public class CompleteStep
        {
            public int TaskId { get; set; }
            public int MainStepId { get; set; }
            public string MainStepName { get; set; }
            public DateTime DateTimeComplete { get; set; }
            public double TotalTime { get; set; }
        }
    }
}