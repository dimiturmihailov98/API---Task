using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace API
{
    public class Tests
    {
        public RestClient client;
        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://taskboard.imsopro66.repl.co/api");
        }

        [Test]
        public void FirstTaskTest()
        {
            RestRequest request = new RestRequest("/tasks/board/Done", Method.GET);

            IRestResponse<List<Task>> response = client.Execute<List<Task>>(request);

            List<Task> tasks = response.Data;

            tasks.ForEach(TestContext.Out.WriteLine);

            Assert.That(tasks[0].title, Is.EqualTo("Project skeleton"));
        }

        [Test]
        public void SearchByKeywordTest()
        {
            RestRequest request = new RestRequest("/tasks/search/home", Method.GET);

            IRestResponse<List<Task>> response = client.Execute<List<Task>>(request);

            Assert.That(response.Data[0].title, Is.EqualTo("Home page"));
        }


        [Test]
        public void SearchMissingKeywordTest()
        {
            RestRequest request = new RestRequest("/tasks/search/missing" + new Random().Next(0, 1000), Method.GET);

            IRestResponse<List<Task>> response = client.Execute<List<Task>>(request);

            Assert.That(response.Data.Count, Is.EqualTo(0));
        }


        [Test]
        public void InvalidContactTest()
        {
            RestRequest request = new RestRequest("/tasks", Method.POST);

            CreateTask newTask = new CreateTask()
            {
                description = "Invalid Task"
            };

            request.AddJsonBody(newTask);
            IRestResponse response = client.Execute(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(400));
            Assert.That(response.StatusDescription, Is.EqualTo("Bad Request"));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void ValidContactTest()
        {
            RestRequest request = new RestRequest("/tasks", Method.POST);

            CreateTask newTask = new CreateTask
            {
                title = "QA exam progress",
                description = "Making exam progress",
                board = "Open"
            };

            request.AddJsonBody(newTask);

            client.Execute(request);

            request = new RestRequest("/tasks/board/Open", Method.GET);

            IRestResponse<List<Task>> response = client.Execute<List<Task>>(request);

            Task lastTask = response.Data[response.Data.Count - 1];

            Assert.That(lastTask.title, Is.EqualTo("QA exam progress"));
            Assert.That(lastTask.description, Is.EqualTo("Making exam progress"));
            Assert.That(lastTask.board.name, Is.EqualTo("Open"));  
        }

    }


}







