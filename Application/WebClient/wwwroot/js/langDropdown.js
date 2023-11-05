let dropdown;

export const initLangDropdown = () => {
  const $targetEl = document.getElementById('langDropdown')
  const $triggerEl = document.getElementById('langDropdownButton')
  dropdown = new Dropdown($targetEl, $triggerEl)
}

export const hideLangDropdown = () => dropdown.hide()