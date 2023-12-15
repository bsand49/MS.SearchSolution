import { render, waitFor, act } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { mockAllIsIntersecting } from 'react-intersection-observer/test-utils';
import nock from 'nock';
import App from './App';

const API_HOST = process.env.REACT_APP_BE_API_URL;
const SEARCH_PERSONS_PATH = process.env.REACT_APP_BE_API_SEARCH_PERSONS_ENDPOINT_PATH
const SEARCH_TERM = 'Search Term';

describe('when app is rendered', () => {
  afterEach(() => {
    nock.cleanAll();
  });

  describe('and user hits search without entering a search term', () => {
    beforeEach(() => {
      nock(API_HOST)
      .defaultReplyHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
      .get(`${SEARCH_PERSONS_PATH}?searchTerm=${SEARCH_TERM}`)
      .reply(200, {
        persons: []
      });
    });

    it('api request is not sent', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(nock.isDone()).not.toBeTruthy();
      });
    });

    it('an error message is displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(getByTestId('search-error')).toBeInTheDocument();
      });
    });

    it('no results found message is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('no-search-results')).toThrow('Unable to find an element by: [data-testid="no-search-results"]');
      });
    });

    it('table is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('persons-table')).toThrow('Unable to find an element by: [data-testid="persons-table"]');
      });
    });
  });

  describe('and user hits search without entering a search term and then enters a search term', () => {
    beforeEach(() => {
      nock(API_HOST)
      .defaultReplyHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
      .get(`${SEARCH_PERSONS_PATH}?searchTerm=${SEARCH_TERM}`)
      .reply(200, {
        persons: []
      });
    });

    it('api request is not sent', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(nock.isDone()).not.toBeTruthy();
      });
    });

    it('an error message appears but then is removed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(getByTestId('search-error')).toBeInTheDocument();
      });

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), 'a');
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('search-error')).toThrow('Unable to find an element by: [data-testid="search-error"]');
      });
    });

    it('no results found message is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('no-search-results')).toThrow('Unable to find an element by: [data-testid="no-search-results"]');
      });

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), 'a');
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('no-search-results')).toThrow('Unable to find an element by: [data-testid="no-search-results"]');
      });
    });

    it('table is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('persons-table')).toThrow('Unable to find an element by: [data-testid="persons-table"]');
      });

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), 'a');
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('persons-table')).toThrow('Unable to find an element by: [data-testid="persons-table"]');
      });
    });
  });

  describe('and user hits search after entering a search term and api returns response container with populated persons array', () => {
    beforeEach(() => {
      nock(API_HOST)
      .defaultReplyHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
      .get(`${SEARCH_PERSONS_PATH}?searchTerm=${SEARCH_TERM}`)
      .reply(200, {
        persons: [
          {
            id: 1,
            first_name: 'Test',
            last_name: 'Person',
            email: 'testperson1@email.com',
            gender: 'Male'
          },
          {
            id: 2,
            first_name: 'Test',
            last_name: 'Person',
            email: 'testperson2@email.com',
            gender: 'Male'
          },
          {
            id: 3,
            first_name: 'Test',
            last_name: 'Person',
            email: 'testperson3@email.com',
            gender: 'Male'
          }
        ]
      });
    });

    it('api request sent', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(nock.isDone()).toBeTruthy();
      });
    });

    it('an error message is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('search-error')).toThrow('Unable to find an element by: [data-testid="search-error"]');
      });
    });

    it('no results found message is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('no-search-results')).toThrow('Unable to find an element by: [data-testid="no-search-results"]');
      });
    });

    it('table is displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(getByTestId('persons-table')).toBeInTheDocument();
      });
    });
  });

  describe('and user hits search after entering a search term and api returns response container with empty persons array', () => {
    beforeEach(() => {
      nock(API_HOST)
      .defaultReplyHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
      .get(`${SEARCH_PERSONS_PATH}?searchTerm=${SEARCH_TERM}`)
      .reply(200, {
        persons: []
      });
    });

    it('api request is sent', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(nock.isDone()).toBeTruthy();
      });
    });

    it('an error message is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('search-error')).toThrow('Unable to find an element by: [data-testid="search-error"]');
      });
    });

    it('no results found message is displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(getByTestId('no-search-results')).toBeInTheDocument();
      });
    });

    it('table is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('persons-table')).toThrow('Unable to find an element by: [data-testid="persons-table"]');
      });
    });
  });

  describe('and user hits search after entering a search term and api returns response container with null persons array', () => {
    beforeEach(() => {
      nock(API_HOST)
      .defaultReplyHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
      .get(`${SEARCH_PERSONS_PATH}?searchTerm=${SEARCH_TERM}`)
      .reply(200, {
        persons: null
      });
    });

    it('api request is sent', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(nock.isDone()).toBeTruthy();
      });
    });

    it('no results found message is displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(getByTestId('no-search-results')).toBeInTheDocument();
      });
    });
  });

  describe('and user hits search after entering a search term and api returns null response container', () => {
    beforeEach(() => {
      nock(API_HOST)
      .defaultReplyHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
      .get(`${SEARCH_PERSONS_PATH}?searchTerm=${SEARCH_TERM}`)
      .reply(200, null);
    });

    it('api request is sent', () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      waitFor(() => {
        expect(nock.isDone()).toBeTruthy();
      })
    });

    it('an error message is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('search-error')).toThrow('Unable to find an element by: [data-testid="search-error"]');
      });
    });

    it('no results found message is displayed', () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      waitFor(() => {
        expect(getByTestId('no-search-results')).toBeInTheDocument();
      })
    });

    it('table is not displayed', () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      waitFor(() => {
        expect(() => getByTestId('persons-table')).toThrow('Unable to find an element by: [data-testid="persons-table"]');
      })
    });
  });

  describe('and user hits search after entering a search term and api returns a non 200 status', () => {
    beforeEach(() => {
      nock(API_HOST)
      .defaultReplyHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
      .get(`${SEARCH_PERSONS_PATH}?searchTerm=${SEARCH_TERM}`)
      .reply(404, { code: 404, message: 'bad request' });
    });

    it('api request is sent', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(nock.isDone()).toBeTruthy();
      });
    });

    it('an error message is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('search-error')).toThrow('Unable to find an element by: [data-testid="search-error"]');
      });
    });

    it('no results found message is displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(getByTestId('no-search-results')).toBeInTheDocument();
      });
    });

    it('table is not displayed', async () => {
      // Arrange
      const { getByTestId } = render(<App />);

      // Act
      act(() => {
        userEvent.type(getByTestId('search-box'), SEARCH_TERM);
        userEvent.click(getByTestId('search-button'));
      });

      // Assert
      await waitFor(() => {
        expect(() => getByTestId('persons-table')).toThrow('Unable to find an element by: [data-testid="persons-table"]');
      });
    });
  });
})