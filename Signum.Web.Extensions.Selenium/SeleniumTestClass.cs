﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium;
using System.Diagnostics;
using System.Threading;

namespace Signum.Web.Selenium
{
    [TestClass]
    public class SeleniumTestClass
    {
        protected static ISelenium selenium;
        protected static Process seleniumServerProcess;

        protected const string PageLoadTimeout = SeleniumExtensions.DefaultPageLoadTimeout; //1.66666667 minutes

        public SeleniumTestClass()
        {

        }

        public static void LaunchSelenium()
        {
            try
            {
                seleniumServerProcess = SeleniumExtensions.LaunchSeleniumProcess();
                Thread.Sleep(5000);
                selenium = SeleniumExtensions.InitializeSelenium();
            }
            catch (Exception)
            {
                MyTestCleanup();
                throw;
            }
        }

        [ClassCleanup]
        public static void MyTestCleanup()
        {
            try
            {
                if (selenium != null)
                {
                    selenium.Stop();
                    selenium.ShutDownSeleniumServer();
                }
                SeleniumExtensions.KillSelenium(seleniumServerProcess);
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
                throw;
            }
        }
    }
}