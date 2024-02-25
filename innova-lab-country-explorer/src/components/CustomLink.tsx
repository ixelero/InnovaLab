import React from 'react'
import {
  Link as RouterLink,
  LinkProps as RouterLinkProps,
  useNavigate,
} from 'react-router-dom'

interface CustomLinkProps extends RouterLinkProps {
  state?: Record<string, any>
}

const CustomLink: React.FC<CustomLinkProps> = ({ to, state, ...rest }) => {
  const navigate = useNavigate()

  const handleClick = (
    event: React.MouseEvent<HTMLAnchorElement, MouseEvent>,
  ) => {
    event.preventDefault()

    if (state) {
      navigate(to as string, { state })
    } else {
      navigate(to as string)
    }
  }

  return <RouterLink to={to as string} onClick={handleClick} {...rest} />
}

export default CustomLink
