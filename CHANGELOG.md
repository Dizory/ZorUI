# Changelog

All notable changes to ZorUI will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2024-10-04

### Added

#### 🚀 CLI Tool (NEW!)
- `ZorUI.CLI` - Command-line tool for creating projects
- `zorui new` command - Create projects from templates
- `zorui list` command - List available templates  
- `zorui info` command - Show framework information
- **4 project templates:**
  - `desktop` - Cross-platform desktop GUI application
  - `console` - Console application
  - `component` - Component library
  - `full` - Full-featured application with best practices
- Beautiful terminal UI with Spectre.Console
- Installation scripts (install-cli.sh, install-cli.cmd)
- Global .NET tool support

#### 🎨 SkiaSharp Renderer (NEW!)
- `ZorUI.Rendering.Skia` - Cross-platform GUI renderer
- Real window support (Windows, Linux, macOS)
- `SkiaWindow` - Cross-platform window class
- 60 FPS rendering
- GPU acceleration
- Mouse and keyboard events support
- Full component rendering (Text, Button, TextField, etc.)

#### Desktop Sample (NEW!)
- `samples/DesktopApp` - Full desktop GUI application
- Counter with buttons
- Form with validation
- Dark/Light theme switcher
- Cards and badges
- Working on Windows, Linux, macOS

#### Core Framework
- `ZorElement` - Base class for all UI elements
- `ZorComponent` - Base class for stateful components
- `ZorApplication` - Application lifecycle management
- `BuildContext` - Context for accessing framework services
- `RebuildScheduler` - Efficient batched rebuild system
- State management with `GetState` and `SetState`
- Component lifecycle hooks (OnMount, OnUnmount, BeforeBuild, AfterBuild)

#### Layout Components
- `VStack` - Vertical stack layout
- `HStack` - Horizontal stack layout
- `ZStack` - Z-axis overlay layout
- `Container` - Base container with styling
- `Grid` - Grid layout with rows and columns
- `Wrap` - Wrapping flow layout
- `ScrollView` - Scrollable container
- `Spacer` - Flexible space
- `FixedSpacer` - Fixed-size spacer
- `Divider` - Visual separator

#### Primitive Components
- `Text` - Text display with typography controls
- `Button` - Interactive button with variants
- `Image` - Image display with fit options
- `Icon` - Icon display
- `Label` - Form label with accessibility

#### Form Components
- `TextField` - Text input with validation
- `Checkbox` - Boolean checkbox
- `Radio` & `RadioGroup` - Single selection
- `Switch` - Toggle switch
- `Slider` - Range slider

#### Navigation Components
- `Tabs` - Tabbed navigation
- `Menu` & `MenuItem` - Dropdown menu
- Menu separators and nested menus

#### Overlay Components
- `Dialog` - Modal dialog
- `AlertDialog` - Alert/confirmation dialog
- `Popover` - Floating popover
- `Tooltip` - Hover tooltip
- `Toast` & `ToastManager` - Notification system

#### Data Display Components
- `Card` - Content card with variants
- `Accordion` & `AccordionItem` - Collapsible sections
- `Progress` - Progress bar/indicator
- `Avatar` - User avatar with status
- `Badge` - Label badge
- `Alert` - Alert message box
- `Spinner` - Loading spinner

#### Styling System
- `Style` - Style object with properties
- `Theme` - Complete theme system
- `ColorPalette` - Semantic color system
- `Typography` - Font settings
- `SpacingScale` - Consistent spacing
- `RadiusScale` - Border radius scale
- `ShadowScale` - Shadow definitions
- Light and Dark themes out of the box
- `Color` - Color class with HSL, RGB, HEX support
- `EdgeInsets` - Padding/margin values
- `Size` & `SizeConstraints` - Size definitions

#### Rendering
- `IRenderer` - Renderer interface
- `ConsoleRenderer` - Console-based renderer for testing

#### Developer Experience
- Fluent API for all components
- IntelliSense-friendly method chaining
- Comprehensive XML documentation
- Type-safe component props
- Hot-reload support (via rebuild system)

### Documentation
- Complete README with examples
- Getting Started guide
- Core Concepts documentation
- Styling guide
- State Management guide
- Best Practices guide
- Quick Reference
- API documentation
- Contributing guidelines
- License (MIT)
- **CLI Documentation (NEW!):**
  - CLI_GUIDE.md - Complete CLI guide
  - QUICK_START_CLI.md - Quick start with CLI
  - CLI_README.md - CLI overview
  - README_CLI.md - CLI features
- **Platform Documentation (NEW!):**
  - PLATFORM_GUIDE.md - Complete platform guide
  - PLATFORM_SUPPORT.md - Platform support details
  - FINAL_ANSWER.md - Platform summary
- **Usage Guides (NEW!):**
  - HOW_TO_USE.md - How to create projects
  - START_HERE.md - Entry point for beginners
  - GETTING_STARTED_QUICK.md - 5-minute start

### Samples
- BasicApp - Simple counter application
- ComponentGallery - Showcase of all components

### Infrastructure
- .NET 8.0 target
- Solution structure with multiple projects
- NuGet package configuration
- .gitignore for .NET projects
- EditorConfig for code style

## [Unreleased]

### Planned Features
- Animation system
- Gesture recognition
- SkiaSharp-based renderer
- Platform-specific renderers (Windows, macOS, Linux)
- Virtualized lists for performance
- Drag and drop support
- Context menu system
- File picker components
- Date/time picker components
- Rich text editor
- Data grid component
- Chart components
- Map component
- Video player component
- Audio player component
- Camera component
- Barcode scanner
- Biometric authentication
- Push notifications
- Local storage
- Networking utilities
- Internationalization (i18n)
- Localization (l10n)
- Right-to-left (RTL) support
- Accessibility improvements
- Performance profiling tools
- DevTools integration
- Visual Studio templates
- CLI tool for project scaffolding
- Hot reload improvements
- Unit testing utilities
- Integration testing framework
- Visual regression testing
- Storybook-like component explorer
- Design tokens export
- Figma plugin

### Under Consideration
- Server-side rendering
- WebAssembly support
- Mobile platform support
- Desktop platform support
- Web platform support
- Plugin system
- Third-party component marketplace

---

## Version History

### Version 1.0.0 (2024-10-04)
- Initial release
- 30+ components
- Complete styling system
- Documentation and samples
- MIT License

---

For more information, visit [ZorUI Documentation](https://zorui.dev)
