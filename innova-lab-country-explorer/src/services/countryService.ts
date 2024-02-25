import axios from 'axios'
import { useQuery, UseQueryResult } from 'react-query'

axios.defaults.baseURL = process.env.REACT_APP_COUNTRY_API_BASE_URL

type BaseQueryFunction<T> = (path: string) => Promise<T>

const baseQuery: BaseQueryFunction<any> = (path) =>
  axios.get(path).then((result) => result.data)

export const useCountryData = (): UseQueryResult<any> =>
  useQuery('countryData', () => baseQuery('/countries'))

export const useFlagAndMaps = (name: string): UseQueryResult<any> =>
  useQuery(['flagAndMaps', name], () =>
    baseQuery(`/countries/flag-maps/${name}`),
  )
