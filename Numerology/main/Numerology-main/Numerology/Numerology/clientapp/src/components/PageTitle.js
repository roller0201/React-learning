import React, { useState } from 'react';
import Typography from '@mui/material/Typography';

export const PageTitle = ({title}) => {
    return(
        <Typography variant="h5" className='Page-title'>
            {title}
        </Typography>
    )
}