import React from 'react'
import Box from '@mui/material/Box'
import TextField from '@mui/material/TextField'

export const EnvironmentForm = ({ register, env, required, focus }) => {
  return (
    <Box component="form" autoComplete="off">
      <TextField
        {...register(env + 'host')}
        fullWidth
        className="Controller-text-field"
        label="Host"
        autoFocus={focus ?? false}
        required={required}
      />
      <TextField
        {...register(env + 'username')}
        fullWidth
        className="Controller-text-field"
        label="Username"
        required={required}
      />
      <TextField
        {...register(env + 'password')}
        fullWidth
        className="Controller-text-field"
        type="password"
        label="Password"
        required={required}
      />
      <TextField
        {...register(env + 'db')}
        fullWidth
        className="Controller-text-field"
        label="DB"
        required={required}
      />
    </Box>
  )
}
