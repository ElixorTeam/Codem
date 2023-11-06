let dropdown;

export const initLangDropdown = () => {
  const $targetEl = document.getElementById('langDropdown')
  const $triggerEl = document.getElementById('langDropdownButton')
  const options = {
    placement: 'bottom',
    offsetSkidding: -40,
  }
  dropdown = new Dropdown($targetEl, $triggerEl, options)
}

export const hideLangDropdown = () => dropdown.hide()