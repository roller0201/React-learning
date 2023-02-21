import React from 'react'
import Box from '@mui/material/Box'
import Stepper from '@mui/material/Stepper'
import Step from '@mui/material/Step'
import StepLabel from '@mui/material/StepLabel'
import Button from '@mui/material/Button'
import Typography from '@mui/material/Typography'
import Paper from '@mui/material/Paper'
import { useForm } from 'react-hook-form'
import AppConfigurationService from '../services/AppConfigurationService'
import { EnvironmentForm } from '../components/EnvironmentForm'
import BaseConfigurationForm from '../components/BaseConfigurationForm'
import { toast } from 'react-toastify'

// ? Maybe we should add input validation? For now we only have * char that this is required but without any validation
export const AppConfigPage = () => {
  const steps = ['Base information', 'PROD', 'UAT', 'DEV', 'SANDBOX']
  const [activeStep, setActiveStep] = React.useState(0)
  const [skipped, setSkipped] = React.useState(new Set())
  const { handleSubmit, register, control } = useForm()

  const onSubmit = async (data) => {
    console.log(data)
    const obj = JSON.stringify({
      Username: data.username,
      MEAWeakResponsePercentage: data.meaWeakResponsePercentage,
      XFIWeakJsonPercentage: data.xfiWeakJsonPercentage,
      MEATimeout: data.meaTimeout,
      XFITimeout: data.xfiTimeout,
      AlarmSoundUrl: data.alarmSoundUrl,
      Theme: data.theme,
      Language: data.language,
      Connections: [
        {
          Environment: 'PROD',
          Host: data.prodhost,
          Username: data.produsername,
          Password: data.prodpassword,
          DB: data.proddb,
        },
        {
          Environment: 'UAT',
          Host: data.uathost,
          Username: data.uatusername,
          Password: data.uatpassword,
          DB: data.uatdb,
        },
        {
          Environment: 'DEV',
          Host: data.devhost,
          Username: data.devusername,
          Password: data.devpassword,
          DB: data.devdb,
        },
        {
          Environment: 'SANDBOX',
          Host: data.sandboxhost,
          Username: data.sandboxusername,
          Password: data.sandboxpassword,
          DB: data.sandboxdb,
        },
      ],
    })
    const res = await AppConfigurationService.UpdateConfig(obj)
    if (res.isError) {
      toast.error('Error updating config')
    } else {
      toast.success('Config updated. Application restart')
      console.log('Res update', res)
    }
  }

  const isStepOptional = (step) => {
    return step === 2 || step === 3 || step === 4
  }

  const handleNext = () => {
    let newSkipped = skipped
    setActiveStep((prevActiveStep) => prevActiveStep + 1)
    setSkipped(newSkipped)
  }

  const handleBack = () => {
    setActiveStep((prevActiveStep) => prevActiveStep - 1)
  }

  return (
    <Box className="Config-page">
      <Stepper activeStep={activeStep}>
        {steps.map((label, index) => {
          const stepProps = {}
          const labelProps = {}
          if (isStepOptional(index)) {
            labelProps.optional = (
              <Typography variant="caption">Optional</Typography>
            )
          }
          return (
            <Step key={label} {...stepProps}>
              <StepLabel {...labelProps}>{label}</StepLabel>
            </Step>
          )
        })}
      </Stepper>
      {activeStep === steps.length ? (
        <React.Fragment>
          <Typography sx={{ mt: 2, mb: 1 }}>
            All steps completed - you&apos;re finished
          </Typography>
          <Box sx={{ display: 'flex', flexDirection: 'row', pt: 2 }}>
            <Box sx={{ flex: '1 1 auto' }} />
          </Box>
        </React.Fragment>
      ) : (
        <React.Fragment>
          <Paper className="App-config-paper">
            {activeStep === 0 && (
              <BaseConfigurationForm
                focus={true}
                register={register}
                control={control}
              />
            )}
            {activeStep === 1 && (
              <EnvironmentForm
                focus={true}
                register={register}
                env="prod"
                required={true}
              />
            )}
            {activeStep === 2 && (
              <EnvironmentForm focus={true} register={register} env="uat" />
            )}
            {activeStep === 3 && (
              <EnvironmentForm focus={true} register={register} env="dev" />
            )}
            {activeStep === 4 && (
              <EnvironmentForm focus={true} register={register} env="sandbox" />
            )}
          </Paper>
          <Box sx={{ display: 'flex', flexDirection: 'row', pt: 2 }}>
            <Button
              color="inherit"
              disabled={activeStep === 0}
              onClick={handleBack}
              sx={{ mr: 1 }}
            >
              Back
            </Button>
            <Box sx={{ flex: '1 1 auto' }} />
            {activeStep !== steps.length - 1 && (
              <Button onClick={handleNext}>Next</Button>
            )}
            {activeStep === steps.length - 1 && (
              <Button onClick={handleSubmit(onSubmit)}>Finish</Button>
            )}
          </Box>
        </React.Fragment>
      )}
    </Box>
  )
}
