import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import "@fontsource/inter"; // Regular weight
import "@fontsource/inter/700.css"; // Bold weight

import './index.css'
import App from './App.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
        <App />
      </BrowserRouter>
  </StrictMode>,
)
