import React from 'react';
import logo from '../images/logo.svg';
import { pageLinks } from '../data';
import { socialLinks } from '../data';

export const Navbar = () => {
  return (
    <nav class='navbar'>
      <div class='nav-center'>
        <div class='nav-header'>
          <img src={logo} class='nav-logo' alt='backroads' />
          <button type='button' class='nav-toggle' id='nav-toggle'>
            <i class='fas fa-bars'></i>
          </button>
        </div>
        {/*<!-- left this comment on purpose -->*/}
        {pageLinks.map((link) => {
          return (
            <li key={link.id}>
              <a href={link.href} className='nav-link'>
                {link.text}
              </a>
            </li>
          );
        })}

        <ul class='nav-icons'>
          {socialLinks.map((link) => {
            const { id, href, icon } = link;
            return (
              <li key={id}>
                <a
                  href={href}
                  target='_blank'
                  rel='noreferrer'
                  class='nav-icon'
                >
                  <i class={icon}></i>
                </a>
              </li>
            );
          })}
        </ul>
      </div>
    </nav>
  );
};
