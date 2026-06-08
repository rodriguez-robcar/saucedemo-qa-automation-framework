# E-Commerce Platform Test Automation Framework
Automated UI test framework built with C#, .NET, Selenium WebDriver, and MSTest to validate critical authentication workflows in the SauceDemo web application.

## Overview
Selenium-based automated testing framework designed to validate key functionalities of an e-commerce web application.
This project demonstrates the implementation of a scalable test automation framework using industry-standard design patterns and best practices.

## Application Under Test
The application under test is SauceDemo, a sample e-commerce platform commonly used for automation practice.

**URL**: https://www.saucedemo.com/

## Test Scenarios (sample)

UC-1 Test Login form with empty credentials:
1. Type any credentials into "Username" and "Password" fields.
2. Clear the inputs.
3. Hit the "Login" button.
4. Check the error messages: "Username is required".

UC-2 Test Login form with credentials by passing Username:
1. Type any credentials in username.
2. Enter password.
3. Clear the "Password" input.
4. Hit the "Login" button.
5. Check the error messages: "Password is required".

UC-3 Test Login form with credentials by passing Username & Password:
1. Type credentials in username which are under Accepted username are sections.
2. Enter password as secret sauce.
3. Click on Login and validate the title “Swag Labs” in the dashboard.

## Framework Architecture
The framework follows the Page Object Model (POM) design pattern to improve maintainability, readability, and reusability.

## Key Features
- Page Object Model (POM)
- Data-driven testing
- Parallel test execution
- Structured logging
- Reusable page components
- Fluent assertions
- Cross-browser support
- Scalable test architecture

## Technologies Stack
- **C#**: Programming language.
- **.NET**: Application framework
- **Selenium WebDriver**: Browser automation.
- **MSTest Framework**: Unit test framework.
- **Fluent Assertions**: Readable assertions
- **NLog**: Logging and diagnostics.
- **Edge and Chrome**: Browsers used for testing.

## Design Patterns and Practices
### Page Object Model
All page interactions are encapsulated within dedicated page classes, separating test logic from UI element implementation.

### Data-Driven Testing
Test cases are parameterized using MSTest data providers, allowing multiple datasets to be executed without duplicating test code.

### Parallel Execution
Tests can run concurrently to reduce execution time and improve feedback cycles.

### Logging
NLog provides detailed execution logs to assist with troubleshooting and test result analysis.

## Prerequisites
- .NET SDK 8.0 or later
- Google Chrome or Microsoft Edge
- Visual Studio 2022 (recommended)

## Installation

1. Clone the repository:
   git clone https://github.com/rodriguez-robcar/selenium-project.git

## Author
Roberto Carlos Rodriguez Torres
