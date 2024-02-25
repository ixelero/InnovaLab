module.exports = {
  parser: '@typescript-eslint/parser',
  plugins: ['@typescript-eslint', 'react', 'prettier'],
  extends: ['plugin:react/recommended', 'prettier'],
  rules: {
    'prettier/prettier': 'error',
  },
}
