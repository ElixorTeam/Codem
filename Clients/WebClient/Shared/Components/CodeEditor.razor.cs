using Microsoft.AspNetCore.Components;

namespace WebClient.Shared.Components;

public partial class CodeEditor : ComponentBase
{
    public string EditorCode { get; set; } = 
@"
## Title

```jsx
function Demo() {
  return <div>demo</div>
}
```

```bash
# Not dependent on uiw.
npm install @codemirror/lang-markdown --save
npm install @codemirror/language-data --save
```

[website ulr](https://github.com/gaelj/BlazorCodeMirror6)

```go
package main
import ""fmt""
func main() {
  fmt.Println(""Hello, 世界"")
}
```
";
}