# LINQ to Ektron Search Tests

## Versioning
The AssemblyVersion and AssemblyInformationalVersion will always match the
GoodlyFere.Ektron.Linq AssemblyVersion and AssemblyInformationVersion.

The AssemblyFileVersion will follow this scheme:
- Major version (of GoodlyFere.Ektron.Linq assembly)
- Minor version (of GoodlyFere.Ektron.Linq assembly)
- Test set version (updated when a set of tests is added)
- Bug fix/minor update version (updated when bugs are fixed or minor updates are made in existing tests)

### History
- (1.0.5.3) Added nullable execution tests that show nullref exception from search manager
- (1.0.5.2) Added to NullableTests
- (1.0.5.1) Removed unused "expectedValue" variables from NullableTests
- (1.0.5.0) Added NullableTests
- (1.0.4.0) Added StringMethodTests
- (1.0.3.0) Adding tests for correct inclusion of return properties
- (1.0.2.5) Moved TestExpressionVisitor to GoodlyFere.Ektron.Linq as FormattingExpressionVisitor
- (1.0.2.4) Added tests for scalar result operators
- (1.0.2.3) Use CriteriaGenerator in tests instead of EktronQueryExecutor to follow refactoring in GoodlyFere.Ektron.Linq