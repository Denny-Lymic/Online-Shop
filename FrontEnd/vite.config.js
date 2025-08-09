import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'

// https://vite.dev/config/

export default ({ mode }) => {
  const fileEnv = loadEnv(mode, process.cwd(), '');
  const env = { ...fileEnv, ...process.env };
  const proxyTarget = env.VITE_PROXY_TARGET ?? env.VITE_API_URL;

  console.log(proxyTarget);

  return defineConfig({
    plugins: [
      react(),
      tailwindcss(),
    ],
    server: {
      host: true,
      port: 5173,
      proxy: { '/api': { target: proxyTarget, changeOrigin: true, secure: false } }
    }
  })
}
