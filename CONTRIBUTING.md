# Contributing to Personnel Management Application

Thank you for your interest in contributing to the Personnel Management Application! 🙋

This document provides guidelines and instructions for contributing to the project.

## 🛈 Code of Conduct

This project adheres to a code of conduct. By participating, you are expected to uphold this code.

### Our Standards

- ✅ Being respectful and inclusive
- ✅ Welcoming feedback and criticism constructively
- ✅ Focusing on what is best for the community
- ✅ Showing empathy towards community members

### Unacceptable Behavior

- ❌ Harassment, discriminatory language, or personal attacks
- ❌ Spam or off-topic content
- ❌ Deliberate disruption

## 🧐 How to Contribute

### 1. Reporting Bugs 🐛

Before creating a bug report, please check the [issue list](https://github.com/salehkheiri1995-png/PersonnelManagementApp01/issues) to avoid duplicates.

**When creating a bug report, include:**

- **Clear description** - What is the bug?
- **Steps to reproduce** - How can someone reproduce it?
- **Expected behavior** - What should happen?
- **Actual behavior** - What actually happened?
- **Environment:**
  - Operating System (Windows 7/10/11, etc.)
  - .NET Framework version
  - Visual Studio version
  - Database file size
- **Screenshots or logs** - If applicable

**Example bug report:**

```
Title: Database connection fails on startup

Steps to reproduce:
1. Run application
2. Select database file
3. Wait 5 seconds

Expected: Database loads successfully
Actual: "Connection timeout" error appears

Environment:
- OS: Windows 10
- .NET: 4.7.2
- VS: 2019
```

### 2. Requesting Features 🚀

**When requesting a feature:**

- **Clear title** - Briefly describe the feature
- **Motivation** - Why is this feature needed?
- **Detailed description** - Explain the feature in detail
- **Use cases** - Provide specific examples
- **Additional context** - Any other relevant information

**Example feature request:**

```
Title: Add dark mode theme option

Motivation:
Users working at night find the bright UI tiring.

Description:
Add a toggle in settings to switch between light and dark themes.
Dark theme should use dark background with light text.

Use Cases:
- Evening work sessions
- Reduced eye strain
- Accessibility for visually impaired
```

### 3. Submitting Code Changes 🛠️

#### Fork and Clone

```bash
# 1. Fork the repository on GitHub
# 2. Clone your fork
git clone https://github.com/YOUR_USERNAME/PersonnelManagementApp01.git
cd PersonnelManagementApp01

# 3. Add upstream remote
git remote add upstream https://github.com/salehkheiri1995-png/PersonnelManagementApp01.git
```

#### Create a Branch

```bash
# Create feature branch
git checkout -b feature/your-feature-name

# OR bug fix branch
git checkout -b fix/bug-description

# OR documentation branch
git checkout -b docs/documentation-update
```

**Branch naming convention:**
```
feature/feature-name           # New features
fix/bug-description            # Bug fixes
docs/documentation-name        # Documentation
refactor/improvement-name      # Code refactoring
perf/performance-improvement   # Performance improvements
test/test-name                 # Tests
```

#### Make Changes

1. **Follow code style guidelines** (see [DEVELOPMENT.md](./DEVELOPMENT.md))
2. **Write clean, well-documented code**
3. **Add comments for complex logic**
4. **Use meaningful variable names**

#### Commit Changes

```bash
# Stage changes
git add .

# Commit with descriptive message
git commit -m "type(scope): description"

# Examples:
git commit -m "feat(personnel): Add bulk import functionality"
git commit -m "fix(database): Resolve connection timeout"
git commit -m "docs(readme): Update installation instructions"
```

**Commit message format:**
```
type(scope): subject

body (optional)

footer (optional)
```

**Types:**
- `feat` - New feature
- `fix` - Bug fix
- `docs` - Documentation
- `style` - Code style (formatting, missing semicolons, etc.)
- `refactor` - Code refactoring
- `perf` - Performance improvement
- `test` - Tests
- `chore` - Build, dependencies, etc.

#### Push and Create Pull Request

```bash
# Push your branch
git push origin feature/your-feature-name

# Create Pull Request on GitHub
```

**Pull Request Template:**

```markdown
## Description
Briefly describe what this PR does.

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Related Issue
Closes #(issue number)

## Testing
Describe how to test this change:
1. Step 1
2. Step 2
3. Step 3

## Checklist
- [ ] Code compiles without errors
- [ ] No compiler warnings
- [ ] Follows code style guidelines
- [ ] All methods have XML comments
- [ ] Code is tested
- [ ] No hardcoded values
- [ ] Error handling implemented
- [ ] Parameterized queries used
- [ ] Tests added/updated
- [ ] Documentation updated

## Screenshots (if applicable)
Attach screenshots for UI changes.
```

## 📄 Documentation Guidelines

### When to Update Documentation

- [ ] Adding new features
- [ ] Changing existing behavior
- [ ] Fixing bugs
- [ ] Adding/removing configuration options

### Documentation Files

- **README.md** - Overview, installation, basic usage
- **ARCHITECTURE.md** - Technical design and structure
- **DEVELOPMENT.md** - Developer setup and guidelines
- **CHANGELOG.md** - Version history
- **Code comments** - Inline explanations

### Documentation Format

```markdown
## Feature Name

Brief description of the feature.

### Functionality
- Feature 1
- Feature 2

### How to Use
1. Step 1
2. Step 2

### Example
```csharp
// Code example
```
```

## 🧪 Testing Your Changes

### Before Submitting PR

- [ ] Application compiles without errors
- [ ] No compiler warnings
- [ ] Manual testing completed
- [ ] Edge cases tested
- [ ] Database operations verified
- [ ] Error handling tested
- [ ] No memory leaks

### Test Checklist

```csharp
// Test 1: Normal operation
DbHelper helper = new DbHelper();
DataTable? result = helper.ExecuteQuery(query);
assert(result != null);

// Test 2: Error handling
DbHelper helper = new DbHelper();
DataTable? result = helper.ExecuteQuery("INVALID QUERY");
assert(result == null); // Should handle gracefully

// Test 3: Parameter safety
string maliciousInput = "'; DROP TABLE Personnel; --";
// Should safely escape parameters
```

## 💁 Review Process

### What to Expect

1. **Automated checks** - Code style, compilation
2. **Code review** - Maintainers review your code
3. **Feedback** - Suggestions for improvement
4. **Revision** - Make requested changes
5. **Approval** - When ready, PR gets approved
6. **Merge** - Code is merged into main branch

### Code Review Criteria

- ✅ Code follows style guidelines
- ✅ Changes are well-documented
- ✅ Error handling is proper
- ✅ No security issues
- ✅ Tests are adequate
- ✅ Database queries use parameters
- ✅ No hardcoded values
- ✅ Changes don't break existing features

## 📁 Development Setup

For detailed setup instructions, see [DEVELOPMENT.md](./DEVELOPMENT.md)

**Quick start:**

```bash
# Clone
git clone https://github.com/YOUR_USERNAME/PersonnelManagementApp01.git
cd PersonnelManagementApp01

# Open in Visual Studio
start PersonnelManagementApp.sln

# Build
Ctrl+Shift+B

# Run
F5
```

## 📚 Additional Resources

- [README.md](./README.md) - Project overview
- [ARCHITECTURE.md](./ARCHITECTURE.md) - Technical architecture
- [DEVELOPMENT.md](./DEVELOPMENT.md) - Developer guide
- [CHANGELOG.md](./CHANGELOG.md) - Version history
- [C# Coding Standards](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [Git Documentation](https://git-scm.com/doc)

## ❤️ Thank You!

Thank you for contributing to make this project better! Your efforts are greatly appreciated.

---

**Questions?** Create an [Issue](https://github.com/salehkheiri1995-png/PersonnelManagementApp01/issues) and we'll help!

**Happy Contributing!** 🚀
