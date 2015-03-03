using System;
using System.Collections.Generic;
using SE.Models;

namespace SE.Classes
{
    public class WebApiClass
    {
        public class User
        {
            public Guid ApplicationId { get; set; }
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public bool IsAnonymous { get; set; }
            public DateTime LastActivityDate { get; set; }
            public string Password { get; set; }
        }

        public class CategoryClass
        {
            public string CategoryName { get; set; }
            public int CategoryId { get; set; }
            public List<TaskClass> Tasks { get; set; }
        }
        public class TaskClass
        {
            public int TaskId { get; set; }
            public string TaskName { get; set; }
            public List<MainStepClass> MainSteps { get; set; }

        }

        public class CompletedMainStepClass
        {
            public int MainStepId { get; set; }
        }


        public class MainStepClass
        {
            public int MainStepID { get; set; }
            public string MainStepName { get; set; }
            public string MainStepText { get; set; }
            public string AudioPath { get; set; }
            public string VideoPath { get; set; }
            public int SortOrder { get; set; }
            public List<CompletedMainStepClass> CompletedMainSteps { get; set; }
            public List<DetailedStepClass> DetailedSteps { get; set; }
        }

        public class DetailedStepClass
        {
            public int DetailedStepID { get; set; }
            public string DetailedStepName { get; set; }
            public string DetailedStepText { get; set; }
            public string ImagePath { get; set; }
            public int SortOrder { get; set; }
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
        public class CompletedTask
        {
            public int TaskID { get; set; }
            public string TaskName { get; set; }
            public string AssignedUser { get; set; }
            public DateTime DateTimeComplete { get; set; }
            public float TotalTime { get; set; }
            public float TotalDetailedStepsUsed { get; set; }
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