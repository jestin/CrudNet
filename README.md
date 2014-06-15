CrudNet
============

A simple CRUD repository library for .NET, and implementations for various databases.

The idea is to create a friendly set of interfaces that define the various CRUD pattern methods (Create, Retrieve, Update, Delete).  These interfaces can then be implemented almost entirely in overridable abstract base classes for various types of databases.  With all these provided, all a user of CrudNet must do is override these base classes to define the bare minimum that is required to work with a particular database.  Different databases will require more or less work from users of CrudNet.  For example. MongoDB requires overrides to specify only a connection string and a collection name, where MySql requires a set of commands and an object builder implementation.  However, both implementations will conform to the same interface, and can be used interchangeably or side by side in an application.
