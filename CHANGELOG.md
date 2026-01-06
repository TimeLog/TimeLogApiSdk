# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## How do I make a good changelog?

### Guiding Principles

- Changelogs are for humans, not machines.
- There should be an entry for every single version.
- The same types of changes should be grouped.
- Versions and sections should be linkable.
- The latest version comes first.
- The release date of each version is displayed.
- Mention whether you follow Semantic Versioning.

### Types of changes

- `Added` for new features.
- `Changed` for changes in existing functionality.
- `Deprecated` for soon-to-be removed features.
- `Removed` for now removed features.
- `Fixed` for any bug fixes.
- `Security` in case of vulnerabilities.

## [Unreleased]

## [1.0.0] - 18-12-2025

### Added

- TimeLog Reporting API SDK for querying data (employees, projects, tasks, work units, customers, contacts, allocations, time off registrations)
- TimeLog Transactional API SDK with 11 service handlers:
  - CRM operations (customers, contacts, leads)
  - Expense management
  - Financial operations
  - Help desk ticket management
  - Invoicing operations
  - Organisation management
  - Project management
  - Salary operations
  - Security and authentication
  - Settings management
  - Time tracking
- Interactive API documentation website with method reference, enumerations, and data schemas
- Sample console application with 20+ integration examples
- PowerShell script libraries for administrative tasks
- Support for multiple authentication methods (API credentials and Personal Access Tokens)
- .NET 6 targeting with precompiled DLLs
- Comprehensive XML documentation and code samples
- Docker support for documentation site deployment

<!--
Update below when *actual* new versions appear.
-->

[unreleased]: https://github.com/TimeLog/TimeLogApiSdk/compare/v1.0.0...master
[1.0.0]: https://github.com/TimeLog/TimeLogApiSdk/releases/tag/v1.0.0
