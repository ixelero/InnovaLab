import React from 'react'
import { QueryClient, QueryClientProvider } from 'react-query'
import { ReactQueryDevtools } from 'react-query/devtools'
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom'
import { CountryInfo, CountryTable } from './components'

const queryClient = new QueryClient()

const App: React.FC = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <Router>
        <div>
          <h1>Country Explorer</h1>
          <Routes>
            <Route path="/" element={<CountryTable countries={[]} />} />
            <Route path="/country/:name" element={<CountryInfo />} />
          </Routes>
        </div>
      </Router>
      <ReactQueryDevtools />
    </QueryClientProvider>
  )
}

export default App
