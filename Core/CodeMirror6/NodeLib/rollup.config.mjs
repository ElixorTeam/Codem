import { nodeResolve } from '@rollup/plugin-node-resolve'
import terser from '@rollup/plugin-terser'
import typescript from '@rollup/plugin-typescript'

export default {
  input: './src/index.ts',
  output: {
    dir: '../wwwroot',
    format: 'esm',
    chunkFileNames: '[name].js',
  },
  plugins: [nodeResolve(), typescript(), terser()],
}
