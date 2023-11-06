export const getPreferredTheme = () => {
  if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches)
    return "dark"
  return "light"
}

export const switchTheme = (theme) => {
  const root = document.documentElement
  root.classList.toggle('dark', theme === 'dark')
}
