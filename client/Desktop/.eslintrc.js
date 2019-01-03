module.exports = {
  root: true,
  parserOptions: {
    sourceType: 'module',
    ecmaVersion: 2018,
    // ecmaFeatures: {
    //   spread: true,
    // },
  },
  env: {
    browser: true,
    node: true,
  },
  extends: ['airbnb-base', 'plugin:vue/recommended', 'prettier'],
  globals: {
    __static: true,
  },
  plugins: ['vue'],
  rules: {
    semi: 0,
    'comma-dangle': 0,
    'global-require': 0,
    'import/no-unresolved': 0,
    'no-param-reassign': 0,
    'no-shadow': 0,
    'import/extensions': 0,
    'import/newline-after-import': 0,
    'no-multi-assign': 0,
    'no-underscore-dangle': 0,
    'no-console': 0,
    'linebreak-style': 0,
    'vue/component-name-in-template-casing': 0,
    // allow debugger during development
    'no-unused-vars': process.env.NODE_ENV === 'production' ? 2 : 0,
    'no-debugger': process.env.NODE_ENV === 'production' ? 2 : 0,
  },
  globals: {
    settings: true,
  },
}
