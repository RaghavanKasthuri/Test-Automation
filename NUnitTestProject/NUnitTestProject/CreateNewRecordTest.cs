using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

[TestFixture]
public class CreateNewRecordTest
{
    private IWebDriver driver;    
    private IJavaScriptExecutor js;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver("C:\\Users\\Ragha\\OneDrive\\Desktop\\Projects\\DotNetProject\\NUnitTestProject\\NUnitTestProject");
        js = (IJavaScriptExecutor)driver;
    }
    
    [Test]
    public void createNewRecord()
    {
        // 1) Navigate to the web application
        driver.Navigate().GoToUrl("https://stirling.she-development.net/automation");
        driver.Manage().Window.Maximize();

        // 2) Login to the web application
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        wait.Until(x => x.FindElement(By.Id("username")));
        driver.FindElement(By.Id("username")).SendKeys("RaghavanK");
        wait.Until(x => x.FindElement(By.Id("login")));
        driver.FindElement(By.Id("login")).Click();
        wait.Until(x => x.FindElement(By.Id("password")));
        driver.FindElement(By.Id("password")).SendKeys("SHE778*");
        wait.Until(x => x.FindElement(By.Id("login")));
        driver.FindElement(By.Id("login")).Click();

        // 3) Click on Modules -> Environment -> Air Emissons to create records
        wait.Until(x => x.FindElement(By.LinkText("Modules")));
        driver.FindElement(By.LinkText("Modules")).Click();
        wait.Until(x => x.FindElement(By.LinkText("Environment")));
        driver.FindElement(By.LinkText("Environment")).Click();
        wait.Until(x => x.FindElement(By.LinkText("Air Emissions")));
        driver.FindElement(By.LinkText("Air Emissions")).Click();

        // 4) Create the first new record
        wait.Until(x => x.FindElement(By.LinkText("New Record")));
        driver.FindElement(By.LinkText("New Record")).Click();
        wait.Until(x => x.FindElement(By.Id("SheAirEmissions_Description")));
        driver.FindElement(By.Id("SheAirEmissions_Description")).Click();
        wait.Until(x => x.FindElement(By.Id("SheAirEmissions_Description")));
        driver.FindElement(By.Id("SheAirEmissions_Description")).SendKeys("1st record");
        wait.Until(x => x.FindElement(By.Id("SheAirEmissions_Location")));
        driver.FindElement(By.Id("SheAirEmissions_Location")).SendKeys("1st record");
        js.ExecuteScript("window.scrollBy(0,350)", "");
        wait.Until(x => x.FindElement(By.XPath("//button[@type=\'button\']")));
        driver.FindElement(By.XPath("//button[@type=\'button\']")).Click();
        wait.Until(x => x.FindElement(By.LinkText("28")));
        driver.FindElement(By.LinkText("28")).Click();
        wait.Until(x => x.FindElement(By.CssSelector("li:nth-child(3) > .btn")));
        driver.FindElement(By.CssSelector("li:nth-child(3) > .btn")).Click();

        // 5) Create a second record
        wait.Until(x => x.FindElement(By.LinkText("New Record")));
        driver.FindElement(By.LinkText("New Record")).Click();
        wait.Until(x => x.FindElement(By.Id("SheAirEmissions_Description")));
        driver.FindElement(By.Id("SheAirEmissions_Description")).Click();
        wait.Until(x => x.FindElement(By.Id("SheAirEmissions_Description")));
        driver.FindElement(By.Id("SheAirEmissions_Description")).SendKeys("2nd record");
        wait.Until(x => x.FindElement(By.Id("SheAirEmissions_Location")));
        driver.FindElement(By.Id("SheAirEmissions_Location")).SendKeys("2nd record");   
        js.ExecuteScript("window.scrollBy(0,350)", "");
        wait.Until(x => x.FindElement(By.XPath("//button[@type=\'button\']")));
        driver.FindElement(By.XPath("//button[@type=\'button\']")).Click();
        wait.Until(x => x.FindElement(By.LinkText("28")));
        driver.FindElement(By.LinkText("28")).Click();
        wait.Until(x => x.FindElement(By.CssSelector("li:nth-child(3) > .btn")));
        driver.FindElement(By.CssSelector("li:nth-child(3) > .btn")).Click();

        // 6) Assertions to confirm that the 2nd record is created and displayed on the page
        Boolean IsDisplayed = driver.PageSource.Contains("2nd record");
        Console.WriteLine("2nd record displayed once created? " + IsDisplayed);
        Assert.IsTrue(IsDisplayed);

        // 7) Delete the most recently created record
        wait.Until(x => x.FindElement(By.XPath("//button/span[2]")));
        driver.FindElement(By.XPath("//button/span[2]")).Click();
        wait.Until(x => x.FindElement(By.LinkText("Delete")));
        driver.FindElement(By.LinkText("Delete")).Click();
        wait.Until(x => x.FindElement(By.CssSelector(".ui-button:nth-child(1)")));
        driver.FindElement(By.CssSelector(".ui-button:nth-child(1)")).Click();

        // 8) Assertions to confirm that the 2nd record is deleted fine and is not displayed on the page
        Boolean IsDisplayedAfterDeletion = driver.PageSource.Contains("2nd record");
        Console.WriteLine("2nd record displayed after deletion? " + IsDisplayedAfterDeletion);
        Assert.IsFalse(IsDisplayedAfterDeletion);

        // 9) Logout of the application
        wait.Until(x => x.FindElement(By.CssSelector(".she-user-name")));
        driver.FindElement(By.CssSelector(".she-user-name")).Click();
        wait.Until(x => x.FindElement(By.LinkText("Log Out")));
        driver.FindElement(By.LinkText("Log Out")).Click();
    }

    [TearDown]
    protected void TearDown()
    {
        driver.Quit();
    }
}