﻿using System;
using System.Configuration;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;

namespace iFlog.Tests.Functional.Framework
{
    [Binding]
    public class TestSetup
    {
        [BeforeScenario("web")]
        public void BeforeScenario()
        {
            var objectContainer = ScenarioContext.Current.GetBindingInstance(typeof(IObjectContainer)) as IObjectContainer;

            if (objectContainer == null) 
                throw new NullReferenceException("Object container is null");
            
            IWebDriver driver;

            switch (ConfigurationManager.AppSettings["Browser"].ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new FirefoxDriver();
                    break;
            }
            objectContainer.RegisterInstanceAs(driver);   
        }

        [AfterScenario("ci")]
        public void AfterScenario()
        {
            var objectContainer = ScenarioContext.Current.GetBindingInstance(typeof(IObjectContainer)) as IObjectContainer;

            if(objectContainer== null)
                throw new NullReferenceException("Object container is null");

            objectContainer.Resolve<IWebDriver>().Dispose();
        }
    }
}