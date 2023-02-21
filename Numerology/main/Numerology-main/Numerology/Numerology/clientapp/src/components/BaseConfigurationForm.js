import TextField from '@mui/material/TextField'
import ReactHookFormSelect from '../components/ReactHookFormSelect'
import MenuItem from '@mui/material/MenuItem'
import Box from '@mui/material/Box'

const BaseConfigurationForm = ({ register, control }) => {
  return (
    <Box component="form" autoComplete="off">
      <TextField
        {...register('username')}
        fullWidth
        className="Controller-text-field"
        label="Username"
        autoFocus={true}
        required
      />
      <TextField
        {...register('meaWeakResponsePercentage')}
        fullWidth
        label="MEA weak warning percentage"
        className="Controller-text-field"
        defaultValue={40}
        required
        inputProps={{
          step: 1,
          min: 0,
          max: 99999,
          type: 'number',
        }}
      />
      <TextField
        {...register('xfiWeakJsonPercentage')}
        fullWidth
        label="XFI weak warning percentage"
        defaultValue={40}
        className="Controller-text-field"
        required
        inputProps={{
          step: 1,
          min: 0,
          max: 99999,
          type: 'number',
        }}
      />
      <TextField
        {...register('meaTimeout')}
        fullWidth
        label="MEA timeout"
        defaultValue={40}
        className="Controller-text-field"
        required
        inputProps={{
          step: 1,
          min: 0,
          max: 99999,
          type: 'number',
        }}
      />
      <TextField
        {...register('xfiTimeout')}
        fullWidth
        label="XFI timeout"
        className="Controller-text-field"
        defaultValue={40}
        required
        inputProps={{
          step: 1,
          min: 0,
          max: 99999,
          type: 'number',
        }}
      />
      <TextField
        {...register('alarmSoundUrl')}
        fullWidth
        defaultValue="https://www.soundhelix.com/examples/mp3/SoundHelix-Song-6.mp3"
        className="Controller-text-field"
        label="Alarm sound url"
        required
      />
      <div className="Controller-text-field">
        <ReactHookFormSelect
          id="lang"
          name="language"
          label="Default Language"
          control={control}
          defaultValue=""
        >
          <MenuItem value="pl">Polish</MenuItem>
          <MenuItem value="en">English</MenuItem>
        </ReactHookFormSelect>
      </div>
      <div className="Controller-text-field">
        <ReactHookFormSelect
          id="theme"
          name="theme"
          label="Default Theme"
          control={control}
          defaultValue=""
        >
          <MenuItem value="light">Light</MenuItem>
          <MenuItem value="dark">Dark</MenuItem>
        </ReactHookFormSelect>
      </div>
    </Box>
  )
}

export default BaseConfigurationForm
