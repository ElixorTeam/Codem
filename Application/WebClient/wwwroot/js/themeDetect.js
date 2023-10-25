window.getPreferredTheme = () => {
  if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches)
    return "dark"
  console.log('Light version')
  return "light"
}