import React from 'react'
import { Link, useLocation, useParams } from 'react-router-dom'
import { useFlagAndMaps } from '../services/countryService'
import '../styles.css'
import { Country } from '../types'

const CountryInfo: React.FC = () => {
  const { name } = useParams()
  const location = useLocation()
  const countryInfo = location.state as Country

  const { data: flagAndMap, isLoading: isLoadingFlagAndMap } = useFlagAndMaps(
    name || '',
  )

  if (isLoadingFlagAndMap) {
    return <div className="container mt-5">Loading...</div>
  }

  if (!countryInfo || !flagAndMap) {
    return <div className="container mt-5">Country not found.</div>
  }

  return (
    <div className="container mt-5">
      <Link to="/" className="btn btn-primary mb-3">
        Back to Countries
      </Link>
      <h2>{countryInfo.name}</h2>
      <p>
        <strong>Capital:</strong> {countryInfo.capital}
      </p>
      <p>
        <strong>Currency:</strong> {countryInfo.currency}
      </p>
      <p>
        <strong>Language:</strong> {countryInfo.language}
      </p>
      <p>
        <strong>Region:</strong> {countryInfo.region}
      </p>
      <p>
        <strong>Flag:</strong>{' '}
        <span className="flagValue">{flagAndMap.flag}</span>
      </p>

      <div>
        <p>
          <strong>Google Map Preview:</strong>
        </p>
        <a
          href={`${flagAndMap.maps}`}
          target="_blank"
          rel="noopener noreferrer"
        >
          Open in Google Maps
        </a>
      </div>
    </div>
  )
}

export default CountryInfo
