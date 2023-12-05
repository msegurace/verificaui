// Collection of reusable RegExps
export const regExps: { [key: string]: RegExp } = {
  str: /^[a-zA-Z]/, // only strings
  num: /^\d+$/, // only numbers
  url: /^[A-Za-z][A-Za-z\d.+-]*:\/*(?:\w+(?::\w+)?@)?[^\s/]+(?::\d+)?(?:\/[\w#!:.?+=&%@\-/]*)?$/
};

// collection of reusable error messages
export const errorMessages: { [key: string]: string } = {
  descripción: 'El campo descripción es requerido.',
  url: 'El campo descripción es requerido.',
  url_malformed: 'No es una url correcta.',
  origen: 'El campo origen es requerido.',
  ens: 'El campo clasificación ENS es requerido.',
}
