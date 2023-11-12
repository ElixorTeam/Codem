let modals = {}

export const initModal = (modalName, uniqueId) => {
  const $targetEl = document.getElementById(modalName)
  const options = {
    backdrop: 'static',
    backdropClasses: 'modalBackground',
  }
  modals[uniqueId] = new Modal($targetEl, options)
}

export const toggleModal = (uniqueId) => {
  if (modals[uniqueId] === null) return
  modals[uniqueId].toggle()
}