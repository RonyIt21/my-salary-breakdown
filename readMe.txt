This solution has following structure

- TestHarness
This calls startup and run demo

- Starup 
This takes care of config, app settings and di 

- Business.Contract
This sets up contract for business component

- Business
This implements business components contract and can be consumed by any ui
It runs deduction calculator based on rules design pattern

- Service.Contract
This sets up contract for ui services

- Service
This implements ui-services contract which can be consumed by any test harness

- Common
This is common stuff across most solution projets
 
 - Core
 This provides core capabilities can be used by components and services

Extensibility

1. Year (past and future):

Change app settings - tax year and supper rate. Also add more as needed.
You can move rules hardcoded in a seperate store or database
You can override virtual method within base class, if different calculation logic is required

 
2. Changes in existing rules
All you need to do is locate corresponding year rules and amend those.
You can easily addd more rules and stich it to calculator
You can change whole deduction calculator alltogether with another implementation

More?
I could include automated test behaviour to have a list of test cases run thru stub instead of asking user input
I Could have better implementation of IExceptionHandler class which would reference IExceptionMapper that maps business messages from xml file for display. 
  Additionally, exception hander could write original exception to file or event source depending on level of logging configured etc
I could put tracing information to external file to enhance operational capability.