import React from 'react'
import { CustomLink } from '.'
import { useCountryData } from '../services/countryService'
import { Country } from '../types'

interface CountryTableProps {
  countries: Country[]
}

const CountryTable: React.FC<CountryTableProps> = ({ countries }) => {
  const { data: countryData, isLoading: isLoadingCountryData } =
    useCountryData()

  if (isLoadingCountryData) {
    return <div className="container mt-5">Loading...</div>
  }

  const data = countryData || []

  return (
    <table className="table">
      <thead>
        <tr>
          <th>Name</th>
          <th>Capital</th>
          <th>Currency</th>
          <th>Language</th>
          <th>Region</th>
        </tr>
      </thead>
      <tbody>
        {data.map((country: any, index: any) => (
          <tr key={index}>
            <td>
              <CustomLink
                to={`/country/${country.name}`}
                state={{
                  name: country.name,
                  capital: country.capital,
                  currency: country.currency,
                  language: country.language,
                  region: country.region,
                }}
              >
                {country.name}
              </CustomLink>
            </td>
            <td>{country.capital}</td>
            <td>{country.currency}</td>
            <td>{country.language}</td>
            <td>{country.region}</td>
          </tr>
        ))}
      </tbody>
    </table>
  )
}

export default CountryTable
