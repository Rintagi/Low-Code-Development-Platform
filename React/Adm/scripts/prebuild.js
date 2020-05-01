'use strict';

process.env.BABEL_ENV = 'production';
process.env.NODE_ENV = 'production';

// Makes the script crash on unhandled rejections instead of silently
// ignoring them. In the future, promise rejections that are not handled will
// terminate the Node.js process with a non-zero exit code.
process.on('unhandledRejection', err => {
  throw err;
});


// Ensure environment variables are read.
require('../config/env');

const cpx = require('cpx');

/* inter-module copy of shared services call, such as Cmn */
/*
cpx.copy('../Cmn/src/services/*.js','./src/services',{update:true,preserve:true,dereference:true},(error)=>{
  console.log('cpx done for Cmn services');
})
*/

/* revised common files from Rintagi, only if newer, i.e. update:true */
cpx.copy('../Shared/src/**/*.js','./src',{update:true,preserve:true,dereference:true},(error)=>{
  console.log('cpx done for shared components');

  /* must be done AFTER shared ones as this is a final overwrite and cpx.copy is async

  /* update local site data(image etc.) 
  cpx.copy('../Site/src/img/**','./src/img',{update:false,preserve:true,dereference:true},(error)=>{
    console.log('cpx done for site react components');
  })
  */

  /* update local site data(configuration etc.) 
  cpx.copy('../Site/public/**','./public',{update:false,preserve:true,dereference:true},(error)=>{
    console.log('cpx done for site public contents');
  })
  */

  /* update local Login.js etc. always regardless of file timestamp 
  cpx.copy('../Site/src/**','./src',{update:false,preserve:true,dereference:true},(error)=>{
    console.log('cpx done for site local js override');
  })
  */

})
