/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
    "./**/*.{razor,html,cshtml}",
    "./node_modules/flowbite/**/*.{js,razor,html,cshtml}"
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('flowbite/plugin'),
    require('tailwind-scrollbar')
  ],
}

