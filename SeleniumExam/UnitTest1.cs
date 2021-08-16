using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Selenium_Tests
{
    public class Tests
    {
        public IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.Navigate().GoToUrl("https://taskboard.imsopro66.repl.co");
        }



        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        
        }

        [Test]
        public void FirstTaskBoard()
        {
            
            driver.FindElement(By.LinkText("Task Board")).Click();
            string title =driver.FindElement(By.Id("task1")).FindElements(By.ClassName("title"))[0].FindElement(By.TagName("td")).Text;
            TestContext.Out.WriteLine(title);
            Assert.That(title, Is.EqualTo("Project skeleton"));
        }

        [Test]
        public void KeywordTest()
        {
            
            driver.FindElement(By.LinkText("Search")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("keyword")).SendKeys("home");
            driver.FindElement(By.Id("Search")).Click();
            string keyword = driver.FindElement(By.Id("task2")).FindElements(By.ClassName("title"))[0].FindElement(By.TagName("td")).Text;
            TestContext.Out.WriteLine(keyword);
            Assert.That(keyword, Is.EqualTo("Home page"));
        }

        [Test]
        public void NoTasksFoundTest()
        {
            
            driver.FindElement(By.LinkText("Search")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("keyword")).SendKeys("missing123");
            driver.FindElement(By.Id("Search")).Click();
            Thread.Sleep(2000);
            string searchResult = driver.FindElement(By.Id("searchResult")).Text;
            TestContext.Out.WriteLine(searchResult);
            Assert.That(searchResult, Is.EqualTo("No tasks found."));
        }

        [Test]
        public void InvalidTasksTest()
        {
            
            driver.FindElement(By.LinkText("Create")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("title")).SendKeys(" ");
            driver.FindElement(By.Id("create")).Click();
            Thread.Sleep(2000);
            string error=driver.FindElement(By.ClassName("err")).Text;
            TestContext.Out.WriteLine(error);
            Assert.That(error, Is.EqualTo("Error: Title cannot be empty!"));
        }

        [Test]
        public void ValidTasksTest()
        {
            driver.FindElement(By.LinkText("Create")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("title")).SendKeys("QA progress");
            driver.FindElement(By.Id("description")).SendKeys("Making progrees with QA");
            driver.FindElement(By.Id("boardName")).SendKeys("Done");
            driver.FindElement(By.Id("create")).Click();
            Thread.Sleep(2000);
            
            IWebElement doneColomn= driver.FindElement(By.XPath("//div[.//h1[text()='Done']]"));
            IReadOnlyList<IWebElement> doneColomnTasks = doneColomn.FindElements(By.TagName("table"));
            IWebElement lastTask = doneColomnTasks[doneColomnTasks.Count - 1];
            string lastTaskTitle = lastTask.FindElement(By.ClassName("title")).FindElement(By.TagName("td")).Text;
            string lastTaskDescription = lastTask.FindElement(By.ClassName("description")).FindElement(By.TagName("td")).Text;
            TestContext.Out.WriteLine(lastTaskTitle);
            TestContext.Out.WriteLine(lastTaskDescription);
            Thread.Sleep(1000);
            Assert.That(lastTaskTitle, Is.EqualTo("QA progress"));
            Assert.That(lastTaskDescription, Is.EqualTo("Making progrees with QA"));
        }







    }

}