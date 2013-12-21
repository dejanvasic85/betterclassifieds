﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paramount.Betterclassifieds.Console;

namespace Paramount.Betterclassifieds.Tests.Console
{
    [TestClass]
    public class TaskArgumentsTests
    {
        [TestMethod]
        public void FromArray_OneItemInArray_ThrowsArgumentException()
        {
            // Arrange
            var args = new[] { "Value" };

            // Act and Assert
            Expect.Exception<ArgumentException>(() => TaskArguments.FromArray(args), "Number of arguments is invalid.");
        }

        [TestMethod]
        public void FromArray_ThreeItemsInArray_ThrowsArgumentException()
        {
            // Arrange
            var args = new[] { TaskArguments.TaskFullArgName, "CoolTaskName", "-FirstCmdArg" };

            // Act and Assert
            Expect.Exception<ArgumentException>(() => TaskArguments.FromArray(args), "Number of arguments is invalid.");
        }

        [TestMethod]
        public void FromArray_NoTaskNameInArray_ThrowsArgumentExeption()
        {
            // Arrange
            var args = new[] { "Arg1", "Val" };

            // Act and Assert
            Expect.Exception<ArgumentException>(
                () => TaskArguments.FromArray(args),
                string.Format("Task name argument must be supplied in format of {0} <Name>. Unable to start program.", TaskArguments.TaskFullArgName));
        }

        [TestMethod]
        public void FromArray_NoArgs_ThrowsArgumentExeption()
        {
            // Arrange
            var args = new string[] { };

            // Act and Assert
            Expect.Exception<ArgumentException>(() => TaskArguments.FromArray(args), "Number of arguments is invalid.");
        }

        [TestMethod]
        public void FromArray_NullArgs_ThrowsArgumentExeption()
        {
            // Arrange
            string[] args = null;

            // Act and Assert
            // ReSharper disable ExpressionIsAlwaysNull
            Expect.Exception<ArgumentException>(() => TaskArguments.FromArray(args), "Number of arguments is invalid.");
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        public void FromArray_TaskNameValueNotSupplied_ThrowsArgumentException()
        {
            // Arrange
            var args = new[] { TaskArguments.TaskFullArgName, "" };

            // Act and Assert
            Expect.Exception<ArgumentException>(
                () => TaskArguments.FromArray(args),
                "Task name has no value supplied.");
        }

        [TestMethod]
        public void FromArray_TaskWithNoOtherArgs_IsEmpty()
        {
            // Arrange 
            var args = new[] { TaskArguments.TaskFullArgName, "CoolTaskName" };

            // Act
            TaskArguments taskArguments = TaskArguments.FromArray(args);

            // Assert
            Assert.AreEqual("CoolTaskName", taskArguments.TaskName);
            Assert.AreEqual(0, taskArguments.Count);
        }

        [TestMethod]
        public void FromArray_TaskNameWithOneArg_HasOneItem()
        {
            // Arrange 
            var args = new[] { TaskArguments.TaskFullArgName, "CoolTaskName", "-Arg1", "Random" };

            // Act
            TaskArguments taskArguments = TaskArguments.FromArray(args);

            // Assert
            Assert.AreEqual("CoolTaskName", taskArguments.TaskName);
            Assert.AreEqual(1, taskArguments.Count);
        }

        [TestMethod]
        public void FromArray_ArgNameNotPrefixed_ThrowsArgumentException()
        {
            // Arrange
            var args = new[] { TaskArguments.TaskFullArgName, "CoolTaskName", "BadArgWithNoDashPrefix", "RandomValue" };

            // Act and Assert
            Expect.Exception<ArgumentException>(
                () => TaskArguments.FromArray(args),
                string.Format("Bad argument {0}. Please specify arguments with a preceding [{1}] and value after it.", "BadArgWithNoDashPrefix", TaskArguments.ArgumentPrefix));
        }

        [TestMethod]
        public void FromArray_TaskNameNotFirstItem_HasOneItem()
        {
            // Arrange 
            var args = new[] { "-Arg1", "Random", TaskArguments.TaskFullArgName, "CoolTaskName" };

            // Act
            TaskArguments taskArguments = TaskArguments.FromArray(args);

            // Assert
            Assert.AreEqual("CoolTaskName", taskArguments.TaskName);
            Assert.AreEqual(1, taskArguments.Count);
        }

        [TestMethod]
        public void FromArray_TenArguments_HasFourItems()
        {
            // Arrange 
            var args = new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Runner", @"{folder}\nunit.exe",
                    "-Folder", @"\\devfoweb01\d$\UHG\Applications\CI\medEbridgeFunctionalTests",
                    "-DLL", "FunctionalTests.BDD.Features.dll",
                    "-Categories", "ci"
                };

            // Act
            TaskArguments taskArguments = TaskArguments.FromArray(args);

            // Assert
            Assert.AreEqual("TestAutomationRun", taskArguments.TaskName);
            Assert.AreEqual(4, taskArguments.Count);
        }

        [TestMethod]
        public void ReadArgument_RequiredArgNotExistsNoDefaultSupplied_ThrowsArgumentException()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Runner", @"nunit.exe"
                });

            // Act 
            // Assert
            Expect.Exception<ArgumentException>(() => taskArguments.ReadArgument("blah", isRequired: true));
        }

        [TestMethod]
        public void ReadArgument_RequiredArgNotExistsUseDefault_ThrowsArgumentException()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Runner", @"nunit.exe"
                });

            // Act 
            Expect.Exception<ArgumentException>(() => taskArguments.ReadArgument("blah", isRequired: true, readDefault: () => "defaultValue"));
        }

        [TestMethod]
        public void ReadArgument_ArgNotExistsUseDefault_ReturnsDefault()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Runner", @"nunit.exe"
                });

            // Act 
            var value = taskArguments.ReadArgument("blah", readDefault: () => "defaultValue");

            // Assert
            Assert.AreEqual("defaultValue", value);
        }

        [TestMethod]
        public void ReadArgument_IntegerArgNotExistsUseDefault_ReturnsDefault()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Runner", @"nunit.exe"
                });

            // Act 
            var value = taskArguments.ReadArgument("blah", readDefault: () => 1);

            // Assert
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void ReadArgument_ArgExists_ReturnsArgValue()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Runner", @"nunit.exe"
                });

            // Act 
            var value = taskArguments.ReadArgument("Runner");

            // Assert
            Assert.AreEqual("nunit.exe", value);
        }

        [TestMethod]
        public void ReadArgument_NonRequiredArgNotExists_ReturnsNull()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Runner", @"nunit.exe"
                });

            // Act 
            var value = taskArguments.ReadArgument("DoesNotExistAndOptional");

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ReadArgument_ArgIsIntegerIsAvailable_ReturnsInteger()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Number", "1"
                });

            // Act 
            var value = taskArguments.ReadArgument<int>("Number");

            // Assert
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void ReadArgument_ArgIsIntegerNotExistsNoDefault_ThrowsArgumentException()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Number", "1"
                });

            // Act 
            Expect.Exception<ArgumentException>(() => taskArguments.ReadArgument<int>("NotValidArgName"));
        }

        [TestMethod]
        public void ReadArgument_ArgValueCannotCast_ThrowsFormatException()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Number", "bullshit"
                });

            // Act 
            Expect.Exception<FormatException>(() => taskArguments.ReadArgument<int>("Number"));
        }

        [TestMethod]
        public void ReadArgument_WithOneEmbeddedValue_DoesSubsitution()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Arg1", "America",
                    "-Arg2", "Captain - {Arg1}"
                });

            // Act 
            var value = taskArguments.ReadArgument("Arg2");

            // Assert
            Assert.AreEqual("Captain - America", value);
        }

        [TestMethod]
        public void ReadArgument_WithMultipleEmbeddedValues_DoesSubsitution()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Arg1", "America",
                    "-Arg2", "Captain - {Arg1} {Final}",
                    "-Final", "Is Number One"
                });

            // Act 
            var value = taskArguments.ReadArgument("Arg2");

            // Assert
            Assert.AreEqual("Captain - America Is Number One", value);
        }

        [TestMethod]
        public void ReadArgument_WithOneEmbeddedValueCaseSensitive_DoesNotDoSubsitution()
        {
            // Arrange
            TaskArguments taskArguments = TaskArguments.FromArray(new[]
                {
                    TaskArguments.TaskFullArgName, "TestAutomationRun",
                    "-Arg1", "America",
                    "-Arg2", "Captain - {arg1}"
                });

            // Act 
            var value = taskArguments.ReadArgument("Arg2");

            // Assert
            Assert.AreEqual("Captain - {arg1}", value);
        }
    }
}
