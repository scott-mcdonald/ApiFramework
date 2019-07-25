# ApiFramework

> **ApiFramework** is a cross-platform, high-performance, open-source framework for producing or consuming modern, Internet-based APIs from both client and server perspectives.

This README is a **work in progress**...

## Overview

TBD for verbiage but keep the following in mind:

- Architecture/Design inspired by the following:
    - EntityFrameworkCore
    - {json:api}
    - GraphQL
- API specification agnostic but with implementations for
    - {json:api}
    - GraphQL
- JSON serializer and deserializer agnostic but with implementations for
    - JSON.NET
    - fastJSON
- Logger agnostic but with implementations for
    - ASP.NET Core
    - NLog
    - Serilog
- TBD...

### Benefits and Features
TBD for verbiage but keep the following in mind:
- Support for **cross-platform** development with **ApiFramework** binaries compiled as [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) 2.0 class libraries
- Fast reading and writing of API requests and responses
    - Internally uses a specialized DOM (**D**ocument **O**bject **M**odel) tree representing the API requests and responses in memory.
    - Internally uses *compiled* .NET expressions for **fast conversion** between API requests and responses into POCO objects.
- TBD...

### Extension Points
TBD for verbiage but keep the following in mind:
- Support for custom type coercion 
- TBD...

## Examples

## License

Licensed under the Apache License, Version 2.0. See `License.md` in the project root for license information.

