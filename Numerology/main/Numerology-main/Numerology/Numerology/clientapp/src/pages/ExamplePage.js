import React, { useState } from 'react'
import logo from '../logo.svg'
import Button from '@mui/material/Button'
import Paper from '@mui/material/Paper'
import { useSelector, useDispatch } from 'react-redux'
import { decrement, increment } from '../services/CounterSlice'
import API from '../services/API'
import ApplicationBar from '../components/ApplicationBar'

export const ExamplePage = ({ changeTheme }) => {
  const [greeting, setGreeting] = useState('Hi...?')

  const openNewElectronWindow = async () => {
    const res = await API().get('test')
    if (res.status === 200) setGreeting('It works!')
    else setGreeting('Nope, try again!')
  }

  const testDBDI = async () => {
    const res = await API().get('window/test/test')
  }

  // Redux
  const count = useSelector((state) => state.counter.value)
  const dispatch = useDispatch()

  const close = () => {
    if (process.env.REACT_APP_MYVAR === 'win') {
      const { remote } = window.require('electron')
      console.log('Electron', remote)
      const win = remote.getCurrentWindow()

      win.minimize()
    }
  }

  return (
    <header className="App-header">
      <img src={logo} className="App-logo" alt="logo" />
      <span>{greeting}</span>
      <div className="test"></div>
      <Button variant="contained" onClick={() => close()}>
        Close
      </Button>
      <Button variant="contained" onClick={() => openNewElectronWindow()}>
        Open new window
      </Button>
      <Button variant="contained" onClick={() => changeTheme()}>
        Change theme
      </Button>
      <Paper variant="contained">Cosik</Paper>
      <div>
        <Button
          variant="contained"
          aria-label="Increment value"
          onClick={() => dispatch(increment())}
        >
          Increment
        </Button>
        <span>{count}</span>
        <Button
          variant="contained"
          aria-label="Decrement value"
          onClick={() => dispatch(decrement())}
        >
          Decrement
        </Button>
      </div>
      <div>
        <Button
          variant="contained"
          aria-label="test db config"
          onClick={() => testDBDI()}
        >
          TEST DB DI
        </Button>
      </div>
    </header>
  )
}
